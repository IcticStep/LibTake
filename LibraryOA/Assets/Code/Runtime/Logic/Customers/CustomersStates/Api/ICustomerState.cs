namespace Code.Runtime.Logic.Customers.CustomersStates.Api
{
    public interface ICustomerState : IExitableCustomerState
    {
        void Start();
    }
}