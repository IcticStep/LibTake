using Code.Runtime.Services.CustomersQueue;
using UnityEngine;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal class BookReceivingState : ICustomerState
    {
        private readonly CustomerStateMachine _customerStateMachine;
        private readonly ICustomersQueueService _customersQueueService;

        public BookReceivingState(CustomerStateMachine customerStateMachine, ICustomersQueueService customersQueueService)
        {
            _customerStateMachine = customerStateMachine;
            _customersQueueService = customersQueueService;
        }

        public void Start() =>
            Debug.Log($"Book receiving state entered. Gameobject name: {_customerStateMachine.gameObject.name}");

        public void Exit() =>
            _customersQueueService.Dequeue();
    }
}