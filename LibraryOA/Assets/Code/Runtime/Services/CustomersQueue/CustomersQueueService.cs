using Code.Runtime.Logic.Queue;
using JetBrains.Annotations;

namespace Code.Runtime.Services.CustomersQueue
{
    [UsedImplicitly]
    internal sealed class CustomersQueueService : ICustomersQueueService
    {
        private Queue _queue;

        public void Initialize(Queue queue) =>
            _queue = queue;

        public void CleanUp() =>
            _queue = null;
    }
}