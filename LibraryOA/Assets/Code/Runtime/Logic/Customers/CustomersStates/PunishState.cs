using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Code.Runtime.Services.Player.Lives;
using UnityEngine;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    public sealed class PunishState : ICustomerState
    {
        private readonly IBookReceiver _bookReceiver;
        private readonly IProgress _progress;
        private readonly IPlayerLivesService _playerLivesService;
        private readonly ICustomerStateMachine _customerStateMachine;

        public PunishState(ICustomerStateMachine customerStateMachine, IBookReceiver bookReceiver, IProgress progress,
            IPlayerLivesService playerLivesService)
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
            _customerStateMachine.NotifyFailed();
            Debug.Log("Receiving failed. Customer unsatisified.");
        }
    }
}