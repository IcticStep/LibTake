using System.Collections.Generic;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal sealed class GoAwayState : ICustomerState
    {
        private readonly CustomerStateMachine _customerStateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly CustomerNavigator _customerNavigator;

        public GoAwayState(CustomerStateMachine customerStateMachine, IStaticDataService staticDataService, CustomerNavigator customerNavigator)
        {
            _customerStateMachine = customerStateMachine;
            _staticDataService = staticDataService;
            _customerNavigator = customerNavigator;
        }

        public void Start()
        {
            IReadOnlyList<Vector3> exitWay = GetExitWay();
            _customerNavigator.SetDestination(exitWay);
            _customerNavigator.LastPointReached += DeactivatedSelf;
        }

        public void Exit() =>
            _customerNavigator.LastPointReached -= DeactivatedSelf;

        private void DeactivatedSelf() =>
            _customerStateMachine.Enter<DeactivatedState>();

        private IReadOnlyList<Vector3> GetExitWay()
        {
            string currentLevel = SceneManager.GetActiveScene().name;
            return _staticDataService
                .ForLevel(currentLevel)
                .Customers
                .ExitWayPoints;
        }
    }
}