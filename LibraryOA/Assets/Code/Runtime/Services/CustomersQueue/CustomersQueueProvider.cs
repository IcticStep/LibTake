using JetBrains.Annotations;

namespace Code.Runtime.Services.CustomersQueue
{
    [UsedImplicitly]
    internal sealed class CustomersQueueProvider : ICustomersQueueProvider
    {
        public Logic.Customers.CustomersQueue CustomersQueue { get; private set; }

        public void Initialize(Logic.Customers.CustomersQueue customersQueue) =>
            CustomersQueue = customersQueue;

        public void CleanUp() =>
            CustomersQueue = null;
    }
}