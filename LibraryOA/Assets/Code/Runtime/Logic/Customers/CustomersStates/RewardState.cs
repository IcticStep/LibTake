using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Code.Runtime.Logic.Customers.CustomersStates.Data;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Services.Player.Lives;
using UnityEngine;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal sealed class RewardState : IPayloadedCustomerState<BookReceivingResult>
    {
        private readonly BookReceiver _bookReceiver;
        private readonly Progress _progress;
        private readonly IPlayerLivesService _playerLivesService;
        private readonly CustomerStateMachine _customerStateMachine;
        private readonly IPlayerInventoryService _playerInventoryService;
        private readonly IStaticDataService _staticDataService;

        public RewardState(CustomerStateMachine customerStateMachine, BookReceiver bookReceiver, Progress progress, IPlayerLivesService playerLivesService, IPlayerInventoryService playerInventoryService, IStaticDataService staticDataService)
        {
            _bookReceiver = bookReceiver;
            _progress = progress;
            _playerLivesService = playerLivesService;
            _customerStateMachine = customerStateMachine;
            _playerInventoryService = playerInventoryService;
            _staticDataService = staticDataService;
        }

        public void Start(BookReceivingResult payload)
        {
            HandleResult(payload);
            _customerStateMachine.Enter<GoAwayState>();
        }

        public void Exit() { }

        private void HandleResult(BookReceivingResult result)
        {
            switch(result)
            {
                case BookReceivingResult.Failed:
                    PunishForFail();
                    break;
                case BookReceivingResult.Success:
                    RewardForSuccess();
                    break;
            }
        }

        private void PunishForFail()
        {
            _bookReceiver.Reset();
            _progress.Reset();
            Debug.Log("Receiving failed. Customer unsatisified.");
            _playerLivesService.WasteLife();
        }
        
        private void RewardForSuccess()
        {
            _progress.Reset();
            _playerInventoryService.AddCoins(_staticDataService.BookReceiving.BookReceivedReward);
            Debug.Log("Receiving successful. Customer owns a book.");
        }
    }
}