using System;

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
            
        }
        
        public void Exit()
        {
        }
    }
}