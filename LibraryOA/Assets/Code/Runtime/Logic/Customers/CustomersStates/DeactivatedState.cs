using Code.Runtime.Logic.Interactions;
using UnityEngine.AI;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal sealed class DeactivatedState : ICustomerState
    {
        private readonly NavMeshAgent _agent;
        private readonly QueueMember _queueMember;
        private readonly BookStorageHolder _bookStorageHolder;
        private readonly CustomerNavigator _customerNavigator;

        public DeactivatedState(NavMeshAgent navMeshAgent, QueueMember queueMember, BookStorageHolder bookStorageHolder, CustomerNavigator customerNavigator)
        {
            _agent = navMeshAgent;
            _queueMember = queueMember;
            _bookStorageHolder = bookStorageHolder;
            _customerNavigator = customerNavigator;
        }

        public void Start()
        {
            _agent.enabled = false;
            _queueMember.Reset();
            _bookStorageHolder.HardReset();
            _customerNavigator.enabled = false;
        }

        public void Exit()
        {
            _agent.enabled = true;
            _customerNavigator.enabled = true;
        }
    }
}