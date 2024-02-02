namespace Code.Runtime.Infrastructure.GameStates.Api
{
    public interface IPayloadedState<in TPayload> : IExitableState
    {
        public void Start(TPayload payload);
    }
}