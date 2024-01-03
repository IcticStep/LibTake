using System.Collections.Generic;
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

        public GoAwayState(ICustomerStateMachine customerStateMachine, IStaticDataService staticDataService, CustomerNavigator customerNavigator,
            ICustomersQueueService customersQueueService)
        {
            _customerStateMachine = customerStateMachine;
            _staticDataService = staticDataService;
            _customerNavigator = customerNavigator;
            _customersQueueService = customersQueueService;
        }

        public void Start()
        {
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