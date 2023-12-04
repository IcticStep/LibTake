using Code.Runtime.Logic.Interactions;
using UnityEngine.AI;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal sealed class DeactivatedState : ICustomerState
    {
        private readonly QueueMember _queueMember;
        private readonly BookStorage _bookStorage;
        private readonly BookReceiver _bookReceiver;

        public DeactivatedState(QueueMember queueMember, BookStorage bookStorage, BookReceiver bookReceiver)
        {
            _queueMember = queueMember;
            _bookStorage = bookStorage;
            _bookReceiver = bookReceiver;
        }

        public void Start()
        {
            _queueMember.Reset();
            _bookReceiver.Reset();
            _bookStorage.Reset();
        }

        public void Exit()
        {
        }
    }
}