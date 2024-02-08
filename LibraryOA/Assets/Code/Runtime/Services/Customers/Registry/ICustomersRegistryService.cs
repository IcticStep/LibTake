using System.Collections.Generic;
using Code.Runtime.Logic.Customers;

namespace Code.Runtime.Services.Customers.Registry
{
    public interface ICustomersRegistryService
    {
        void Register(CustomerStateMachine customer);
        ICustomerStateMachine GetCustomerByQueueMember(QueueMember queueMember);
        void CleanUp();
        IEnumerable<CustomerStateMachine> AllCustomers { get; }
        void ForceStopAllCustomers();
    }
}