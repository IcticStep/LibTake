namespace Code.Runtime.Logic.Customers.CustomersStates.Api
{
    public interface IPayloadedCustomerState<in TPayload> : IExitableCustomerState
    {
        public void Start(TPayload payload);
    }
}