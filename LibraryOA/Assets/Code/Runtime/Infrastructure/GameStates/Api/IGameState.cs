namespace Code.Runtime.Infrastructure.GameStates.Api
{
    public interface IGameState : IExitableState
    {
        public void Start();
    }
}