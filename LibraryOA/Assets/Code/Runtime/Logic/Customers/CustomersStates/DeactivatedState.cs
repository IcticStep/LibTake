using Code.Runtime.Logic.Interactions;
using UnityEngine.AI;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal sealed class DeactivatedState : ICustomerState
    {
        private readonly QueueMember _queueMember;
        private readonly BookStorageHolder _bookStorageHolder;

        public DeactivatedState(QueueMember queueMember, BookStorageHolder bookStorageHolder)
        {
            _queueMember = queueMember;
            _bookStorageHolder = bookStorageHolder;
        }

        public void Start()
        {
            _queueMember.Reset();
            _bookStorageHolder.HardReset();
        }

        public void Exit()
        {
        }
    }
}