using System;
using System.Collections.Generic;
using Code.Runtime.Logic.Customers.CustomersStates;
using UnityEngine;

namespace Code.Runtime.Logic.Customers
{
    internal sealed class CustomerStateMachine : MonoBehaviour
    {
        [SerializeField]
        private QueueMember _queueMember;
        
        private Dictionary<Type, ICustomerState> _states;
        private ICustomerState _activeState;

        private void Awake() =>
            _states = new Dictionary<Type, ICustomerState>
            {
                [typeof(GoToBookReceivingState)] = new GoToBookReceivingState(this, _queueMember),
                [typeof(BookReceivingState)] = new BookReceivingState(this),
            };

        private void Start() =>
            Enter<GoToBookReceivingState>();

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