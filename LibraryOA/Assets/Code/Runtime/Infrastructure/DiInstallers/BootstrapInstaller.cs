using Code.Runtime.Infrastructure.Services;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Services.InputService;
using Zenject;

namespace Code.Runtime.Infrastructure.DiInstallers
{
    internal sealed class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallInfrastructureServices();
            InstallServices();
            InstallStateMachine();
            InstallGlobalStates();
        }

        private void InstallInfrastructureServices()
        {
            Container.Bind<IInitializable>().To<StateMachineStarter>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }

        private void InstallServices()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle();
        }

        private void InstallStateMachine()
        {
            Container.Bind<IGameStateFactory>().To<GameStateFactory>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }

        private void InstallGlobalStates()
        {
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
        }
    }
}