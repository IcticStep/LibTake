using System;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Customers.CustomersStates;
using Code.Runtime.Services.CustomersQueue;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Customers
{
    [SelectionBase]
    public sealed class CustomerStateMachine : MonoBehaviour
    {
        [SerializeField]
        private QueueMember _queueMember;
        [SerializeField]
        private CustomerNavigator _customerNavigator;
        
        private Dictionary<Type, ICustomerState> _states;
        private ICustomerState _activeState;
        private ICustomersQueueService _customersQueueService;
        private IStaticDataService _staticDataService;

        public string ActiveStateName => _activeState == null ? "none" : _activeState.ToString();

        [Inject]
        private void Construct(ICustomersQueueService customersQueueService, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _customersQueueService = customersQueueService;
        }

        private void Awake() =>
            _states = new Dictionary<Type, ICustomerState>
            {
                [typeof(QueueMemberState)] = new QueueMemberState(this, _queueMember, _customersQueueService, _customerNavigator),
                [typeof(BookReceivingState)] = new BookReceivingState(this),
                [typeof(GoAwayState)] = new GoAwayState(this, _staticDataService, _customerNavigator),
                [typeof(DeactivatedState)] = new DeactivatedState(),
            };

        private void Start() =>
            Enter<QueueMemberState>();

        public void Enter<TState>()
            where TState : ICustomerState
        {
            _activeState?.Exit();
            ICustomerState nextState = _states[typeof(TState)];
            nextState.Start();
            _activeState = nextState;
        }
    }
}