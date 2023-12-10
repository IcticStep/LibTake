using Code.Runtime.Logic.Customers;

namespace Code.Runtime.Services.Customers.Registry
{
    public interface ICustomersRegistryService
    {
        void Register(CustomerStateMachine customer);
        CustomerStateMachine GetCustomerByQueueMember(QueueMember queueMember);
        void CleanUp();
    }
}