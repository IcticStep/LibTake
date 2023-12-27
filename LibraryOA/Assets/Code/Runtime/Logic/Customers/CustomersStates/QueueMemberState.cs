using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Code.Runtime.Services.Customers.Queue;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal sealed class QueueMemberState : ICustomerState
    {
        private readonly ICustomerStateMachine _customerStateMachine;
        private readonly QueueMember _queueMember;
        private readonly ICustomersQueueService _customersQueueService;
        private readonly CustomerNavigator _customerNavigator;

        public QueueMemberState(ICustomerStateMachine customerStateMachine, QueueMember queueMember, ICustomersQueueService customersQueueService,
            CustomerNavigator customerNavigator)
        {
            _customerStateMachine = customerStateMachine;
            _queueMember = queueMember;
            _customersQueueService = customersQueueService;
            _customerNavigator = customerNavigator;
        }

        public void Start()
        {
            _queueMember.Updated += OnQueueMemberUpdated;
            _customerNavigator.PointReached += OnPointReached;
            _customersQueueService.Enqueue(_queueMember);
        }

        public void Exit()
        {
            _queueMember.Updated -= OnQueueMemberUpdated;
            _customerNavigator.PointReached -= OnPointReached;
        }
 
        private void OnQueueMemberUpdated() =>
            WalkToQueuePlace();

        private void OnPointReached()
        {
            if(_queueMember.First)
                _customerStateMachine.Enter<BookReceivingState>();
            else
                WalkToQueuePlace();;
        }

        private void WalkToQueuePlace()
        {
            if(_queueMember.CurrentPoint != null)
                _customerNavigator.SetDestination(_queueMember.CurrentPoint.Value);
        }
    }
}