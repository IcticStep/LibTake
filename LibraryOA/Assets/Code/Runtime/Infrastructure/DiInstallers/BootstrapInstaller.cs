using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Infrastructure.Services;
using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Services.InputService;
using Code.Runtime.Services.Physics;
using Code.Runtime.Services.PlayerProvider;
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
            Container.Bind<IPlayerProgressService>().To<PlayerProgressService>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }

        private void InstallServices()
        {
            Container.Bind<IPhysicsService>().To<PhysicsService>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.BindInterfacesTo<PlayerProviderService>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
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