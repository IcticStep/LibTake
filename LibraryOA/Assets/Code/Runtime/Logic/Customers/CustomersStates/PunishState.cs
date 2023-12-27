using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Code.Runtime.Services.Player.Lives;
using UnityEngine;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal sealed class PunishState : ICustomerState
    {
        private readonly BookReceiver _bookReceiver;
        private readonly Progress _progress;
        private readonly IPlayerLivesService _playerLivesService;
        private readonly CustomerStateMachine _customerStateMachine;

        public PunishState(BookReceiver bookReceiver, Progress progress, IPlayerLivesService playerLivesService,
            CustomerStateMachine customerStateMachine)
        {
            _bookReceiver = bookReceiver;
            _progress = progress;
            _playerLivesService = playerLivesService;
            _customerStateMachine = customerStateMachine;
        }

        public void Start()
        {
            Punish();
            _customerStateMachine.Enter<GoAwayState>();
        }

        public void Exit() { }
        
        private void Punish()
        {
            _bookReceiver.Reset();
            _progress.Reset();
            _playerLivesService.WasteLife();
            Debug.Log("Receiving failed. Customer unsatisified.");
        }
    }
}