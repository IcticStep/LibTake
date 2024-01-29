using System.Collections.Generic;
using Code.Runtime.Logic.Customers;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Customers.Registry
{
    [UsedImplicitly]
    public sealed class CustomersRegistryService : ICustomersRegistryService
    {
        private readonly Dictionary<QueueMember, CustomerStateMachine> _registry = new();

        public void Register(CustomerStateMachine customer) =>
            _registry.Add(
                customer.GetComponentInChildren<QueueMember>(),
                customer);

        public ICustomerStateMachine GetCustomerByQueueMember(QueueMember queueMember) =>
            _registry.GetValueOrDefault(queueMember);

        public void CleanUp() =>
            _registry.Clear();
    }
}