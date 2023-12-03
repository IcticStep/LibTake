using Code.Runtime.Logic.Interactions;
using UnityEngine.AI;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal sealed class DeactivatedState : ICustomerState
    {
        private readonly QueueMember _queueMember;
        private readonly BookStorageHolder _bookStorageHolder;
        private readonly BookReceiver _bookReceiver;

        public DeactivatedState(QueueMember queueMember, BookStorageHolder bookStorageHolder, BookReceiver bookReceiver)
        {
            _queueMember = queueMember;
            _bookStorageHolder = bookStorageHolder;
            _bookReceiver = bookReceiver;
        }

        public void Start()
        {
            _queueMember.Reset();
            _bookReceiver.Reset();
            _bookStorageHolder.Reset();
        }

        public void Exit()
        {
        }
    }
}