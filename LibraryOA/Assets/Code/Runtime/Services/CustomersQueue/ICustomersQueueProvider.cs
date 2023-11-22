namespace Code.Runtime.Services.CustomersQueue
{
    internal interface ICustomersQueueProvider
    {
        Logic.Customers.CustomersQueue CustomersQueue { get; }
        void Initialize(Logic.Customers.CustomersQueue customersQueue);
        void CleanUp();
    }
}