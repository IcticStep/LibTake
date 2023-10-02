namespace Code.Runtime.Infrastructure.States.Api
{
    public interface IPayloadedState<in TPayload> : IExitableState
    {
        public void Start(TPayload payload);
    }
}