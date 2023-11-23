using Code.Runtime.Services.CustomersQueue;
using Zenject;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal sealed class GoToBookReceivingState : ICustomerState
    {
        private readonly CustomerStateMachine _customerStateMachine;
        private readonly QueueMember _queueMember;
        private readonly ICustomersQueueService _customersQueueService;

        public GoToBookReceivingState(CustomerStateMachine customerStateMachine, QueueMember queueMember, ICustomersQueueService customersQueueService)
        {
            _customerStateMachine = customerStateMachine;
            _queueMember = queueMember;
            _customersQueueService = customersQueueService;
        }

        public void Start()
        {
            _customersQueueService.Enqueue(_queueMember);
            _queueMember.BecameFirst += StartReceiving;
        }

        public void Exit() =>
            _queueMember.BecameFirst -= StartReceiving;

        private void StartReceiving() =>
            _customerStateMachine.Enter<BookReceivingState>();
    }
}