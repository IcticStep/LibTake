using System;
using UnityEngine;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal class BookReceivingState : ICustomerState
    {
        private readonly CustomerStateMachine _customerStateMachine;

        public BookReceivingState(CustomerStateMachine customerStateMachine)
        {
            _customerStateMachine = customerStateMachine;
        }

        public void Start()
        {
            Debug.Log($"Book receiving state entered. Gameobject name: {_customerStateMachine.gameObject.name}");
        }
        
        public void Exit()
        {
        }
    }
}