using Code.Runtime.Services.CustomersQueue;
using Zenject;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal sealed class GoToBookReceivingState : ICustomerState
    {
        private readonly CustomerStateMachine _customerStateMachine;
        private readonly QueueMember _queueMember;
        private readonly ICustomersQueueProvider _customersQueueProvider;

        public GoToBookReceivingState(CustomerStateMachine customerStateMachine, QueueMember queueMember, ICustomersQueueProvider customersQueueProvider)
        {
            _customerStateMachine = customerStateMachine;
            _queueMember = queueMember;
            _customersQueueProvider = customersQueueProvider;
        }

        public void Start()
        {
            _customersQueueProvider.CustomersQueue.Enqueue(_queueMember);
            _queueMember.BecameFirst += StartReceiving;
        }

        public void Exit() =>
            _queueMember.BecameFirst -= StartReceiving;

        private void StartReceiving() =>
            _customerStateMachine.Enter<BookReceivingState>();
    }
}