using System;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Code.Runtime.Services.Books.Reward;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Ui.FlyingResources;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    public sealed class RewardState : ICustomerState, IRewardSource
    {
        private readonly IProgress _progress;
        private readonly ICustomerStateMachine _customerStateMachine;
        private readonly IPlayerInventoryService _playerInventoryService;
        private readonly IBookRewardService _bookRewardService;
        private readonly IStaticDataService _staticDataService;
        public event Action<int> Rewarded;

        public RewardState(ICustomerStateMachine customerStateMachine, IProgress progress, IPlayerInventoryService playerInventoryService,
            IBookRewardService bookRewardService, IStaticDataService staticDataService)
        {
            _progress = progress;
            _customerStateMachine = customerStateMachine;
            _playerInventoryService = playerInventoryService;
            _bookRewardService = bookRewardService;
            _staticDataService = staticDataService;
        }

        public void Start()
        {
            Reward();
            FinishWaiting();
            GoToAwayStateAfterDelay().Forget();
        }

        private async UniTask GoToAwayStateAfterDelay()
        {
            await UniTask.WaitForSeconds(_staticDataService.BookReceiving.RewardSecondsDelay);
            _customerStateMachine.Enter<GoAwayState>();
        }

        public void Exit() { }

        private void FinishWaiting() =>
            _progress.Reset();

        private void Reward()
        {
            int reward = _bookRewardService.GetRewardBy(_progress);
            _playerInventoryService.AddCoins(reward);
            Rewarded?.Invoke(reward);
            _customerStateMachine.NotifyReward();
            Debug.Log("Receiving successful. Customer owns a book.");
        }
    }
}