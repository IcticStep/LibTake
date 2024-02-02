namespace Code.Runtime.Infrastructure.GameStates.Api
{
    public interface IState : IExitableState
    {
        public void Start();
    }
}