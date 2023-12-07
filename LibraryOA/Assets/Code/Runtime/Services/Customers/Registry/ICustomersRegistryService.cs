using Code.Runtime.Logic.Customers;

namespace Code.Runtime.Services.Customers.Registry
{
    internal interface ICustomersRegistryService
    {
        void Register(CustomerStateMachine customer);
        CustomerStateMachine GetCustomerByQueueMember(QueueMember queueMember);
        void CleanUp();
    }
}