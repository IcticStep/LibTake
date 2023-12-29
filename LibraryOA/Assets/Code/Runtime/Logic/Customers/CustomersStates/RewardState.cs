using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Code.Runtime.Services.Player.Inventory;
using UnityEngine;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    public sealed class RewardState : ICustomerState
    {
        private readonly IProgress _progress;
        private readonly ICustomerStateMachine _customerStateMachine;
        private readonly IPlayerInventoryService _playerInventoryService;
        private readonly IStaticDataService _staticDataService;

        public RewardState(ICustomerStateMachine customerStateMachine, IProgress progress, IPlayerInventoryService playerInventoryService, IStaticDataService staticDataService)
        {
            _progress = progress;
            _customerStateMachine = customerStateMachine;
            _playerInventoryService = playerInventoryService;
            _staticDataService = staticDataService;
        }
        
        public void Start()
        {
            FinishWaiting();
            Reward();
            _customerStateMachine.Enter<GoAwayState>();
        }

        public void Exit() { }

        private void FinishWaiting() =>
            _progress.Reset();

        private void Reward()
        {
            int rewardSize = _staticDataService.BookReceiving.BookRewards.GetRewardSize(_progress.Value);
            _playerInventoryService.AddCoins(rewardSize);
            Debug.Log("Receiving successful. Customer owns a book.");
        }
    }
}