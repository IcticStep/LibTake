using System.Collections.Generic;
using Code.Runtime.Infrastructure.GameStates;
using Code.Runtime.Infrastructure.GameStates.States;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Code.Runtime.Services.Customers.Queue;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal sealed class GoAwayState : ICustomerState
    {
        private readonly ICustomerStateMachine _customerStateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly CustomerNavigator _customerNavigator;
        private readonly ICustomersQueueService _customersQueueService;
        private readonly GameStateMachine _gameStateMachine;

        public GoAwayState(ICustomerStateMachine customerStateMachine, IStaticDataService staticDataService, CustomerNavigator customerNavigator,
            ICustomersQueueService customersQueueService, GameStateMachine gameStateMachine)
        {
            _customerStateMachine = customerStateMachine;
            _staticDataService = staticDataService;
            _customerNavigator = customerNavigator;
            _customersQueueService = customersQueueService;
            _gameStateMachine = gameStateMachine;
        }

        public void Start()
        {
            if(_gameStateMachine.ActiveStateType != typeof(DayGameState))
                return;
            
            _customersQueueService.Dequeue();
            IReadOnlyList<Vector3> exitWay = GetExitWay();
            _customerNavigator.SetDestination(exitWay, stoppingOnPoints: false);
            _customerNavigator.LastPointReached += DeactivatedSelf;
        }

        public void Exit() =>
            _customerNavigator.LastPointReached -= DeactivatedSelf;

        private void DeactivatedSelf() =>
            _customerStateMachine.Enter<DeactivatedState>();

        private IReadOnlyList<Vector3> GetExitWay() =>
            _staticDataService
                .CurrentLevelData
                .Customers
                .ExitWayPoints;
    }
}