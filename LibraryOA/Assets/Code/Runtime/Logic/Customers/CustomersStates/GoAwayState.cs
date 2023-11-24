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

        public GoAwayState(CustomerStateMachine customerStateMachine, IStaticDataService staticDataService)
        {
            _customerStateMachine = customerStateMachine;
            _staticDataService = staticDataService;
        }

        public async void Start()
        {
            IReadOnlyList<Vector3> exitWay = GetExitWay();
        }

        public void Exit() { }

        private IReadOnlyList<Vector3> GetExitWay()
        {
            LevelStaticData currentLevelData = _staticDataService.ForLevel(SceneManager.GetActiveScene().name);
            IReadOnlyList<Vector3> exitWay = currentLevelData.Customers.ExitWayPoints;
            return exitWay;
        }
    }
}