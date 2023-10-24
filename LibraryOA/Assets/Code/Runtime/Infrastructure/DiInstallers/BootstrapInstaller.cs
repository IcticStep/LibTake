using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Infrastructure.Services.Factories.Interactables;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Services.InputService;
using Code.Runtime.Services.Interactions;
using Code.Runtime.Services.Physics;
using Code.Runtime.Services.Player;
using Zenject;

namespace Code.Runtime.Infrastructure.DiInstallers
{
    internal sealed class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            Container.Bind<IInitializable>().To<BootstrapInstaller>().FromInstance(this).AsSingle();
            InstallInfrastructureServices();
            InstallServices();
            InstallFactories();
            InstallStateMachine();
            InstallGlobalStates();
        }

        public void Initialize()
        {
            Container.Resolve<GameStateMachine>().EnterState<BootstrapState>();
        }

        private void InstallInfrastructureServices()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IPlayerProgressService>().To<PlayerProgressService>().AsSingle();
            Container.Bind<ISaveLoadRegistry>().To<SaveLoadRegistry>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }

        private void InstallServices()
        {
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IPhysicsService>().To<PhysicsService>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IInteractablesRegistry>().To<InteractablesRegistry>().AsSingle();
            Container.Bind<IPlayerProviderService>().To<PlayerProviderService>().AsSingle();
            Container.Bind<IPlayerInventoryService>().To<PlayerInventoryService>().AsSingle();
            Container.Bind<IBookSlotInteractionService>().To<BookSlotInteractionService>().AsSingle();
            Container.Bind<IReadingTableInteractionService>().To<ReadingTableInteractionService>().AsSingle();
            Container.Bind<IReadBookService>().To<ReadBookService>().AsSingle();
        }

        private void InstallFactories()
        {
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
            Container.Bind<IBookSlotFactory>().To<BookSlotFactory>().AsSingle();
            Container.Bind<IReadingTableFactory>().To<ReadingTableFactory>().AsSingle();
        }

        private void InstallStateMachine()
        {
            Container.Bind<IGameStateFactory>().To<GameStateFactory>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }

        private void InstallGlobalStates()
        {
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<WarmupState>().AsSingle();
            Container.Bind<LoadProgressState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
        }
    }
}