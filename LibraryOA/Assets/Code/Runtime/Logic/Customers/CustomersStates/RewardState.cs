using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Code.Runtime.Services.Books.Reward;
using Code.Runtime.Services.Player.Inventory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    public sealed class RewardState : ICustomerState
    {
        private readonly IProgress _progress;
        private readonly ICustomerStateMachine _customerStateMachine;
        private readonly IPlayerInventoryService _playerInventoryService;
        private readonly IBookRewardService _bookRewardService;

        public RewardState(ICustomerStateMachine customerStateMachine, IProgress progress, IPlayerInventoryService playerInventoryService,
            IBookRewardService bookRewardService)
        {
            _progress = progress;
            _customerStateMachine = customerStateMachine;
            _playerInventoryService = playerInventoryService;
            _bookRewardService = bookRewardService;
        }
        
        public void Start()
        {
            Reward();
            FinishWaiting();
            GoToAwayStateAfterDelay().Forget();
        }

        private async UniTask GoToAwayStateAfterDelay()
        {
            await UniTask.Yield();
            _customerStateMachine.Enter<GoAwayState>();
        }

        public void Exit() { }

        private void FinishWaiting() =>
            _progress.Reset();

        private void Reward()
        {
            int reward = _bookRewardService.GetRewardBy(_progress);
            _playerInventoryService.AddCoins(reward);
            Debug.Log("Receiving successful. Customer owns a book.");
        }
    }
}