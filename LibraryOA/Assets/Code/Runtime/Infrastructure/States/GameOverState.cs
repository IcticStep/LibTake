using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.States.Api;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.States
{
    [UsedImplicitly]
    internal sealed class GameOverState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        
        public GameOverState(ISceneLoader sceneLoader) 
        {
            _sceneLoader = sceneLoader;
        }

        public void Start()
        {
        }

        public void Exit()
        {
        }
    }
}