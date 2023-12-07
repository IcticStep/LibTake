using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Infrastructure.Services.Camera;
using Code.Runtime.Infrastructure.Services.CleanUp;
using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Infrastructure.Services.HudProvider;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.Restart;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.Services.UiMessages;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Services.BooksDelivering;
using Code.Runtime.Services.BooksReceiving;
using Code.Runtime.Services.Customers.Delivering;
using Code.Runtime.Services.Customers.Pooling;
using Code.Runtime.Services.Customers.Queue;
using Code.Runtime.Services.Customers.Registry;
using Code.Runtime.Services.Days;
using Code.Runtime.Services.InputService;
using Code.Runtime.Services.Interactions.BookSlotInteraction;
using Code.Runtime.Services.Interactions.BooksReceiving;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.Services.Interactions.ReadingTable;
using Code.Runtime.Services.Interactions.Registry;
using Code.Runtime.Services.Interactions.Truck;
using Code.Runtime.Services.Physics;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Services.Player.Lives;
using Code.Runtime.Services.Player.Provider;
using Code.Runtime.Services.Random;
using Code.Runtime.Services.Skills;
using Code.Runtime.Services.TruckDriving;
using UnityEngine;
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
            Application.targetFrameRate = 60;
            Container.Resolve<GameStateMachine>().EnterState<BootstrapState>();
        }

        private void InstallInfrastructureServices()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IPersistantProgressService>().To<PersistantProgressService>().AsSingle();
            Container.Bind<ISaveLoadRegistry>().To<SaveLoadRegistry>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<ILevelCleanUpService>().To<LevelCleanUpService>().AsSingle();
            Container.Bind<IRestartService>().To<RestartService>().AsSingle();
        }

        private void InstallServices()
        {
            Container.Bind<IPhysicsService>().To<PhysicsService>().AsSingle();
            Container.Bind<IRandomService>().To<RandomService>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IInteractablesRegistry>().To<InteractablesRegistry>().AsSingle();
            Container.Bind<IPlayerProviderService>().To<PlayerProviderService>().AsSingle();
            Container.Bind<IPlayerInventoryService>().To<PlayerInventoryService>().AsSingle();
            Container.Bind<IBookSlotInteractionService>().To<BookSlotInteractionService>().AsSingle();
            Container.Bind<IReadingTableInteractionService>().To<ReadingTableInteractionService>().AsSingle();
            Container.Bind<IReadBookService>().To<ReadBookService>().AsSingle();
            Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle();
            Container.Bind<IBooksDeliveringService>().To<BooksDeliveringService>().AsSingle();
            Container.Bind<ITruckProvider>().To<TruckProvider>().AsSingle();
            Container.Bind<ITruckInteractionService>().To<TruckInteractionService>().AsSingle();
            Container.Bind<ICustomersQueueService>().To<CustomersQueueService>().AsSingle();
            Container.Bind<IBooksReceivingService>().To<BooksReceivingService>().AsSingle();
            Container.Bind<IBooksReceivingInteractionsService>().To<BooksReceivingInteractionsService>().AsSingle();
            Container.Bind<IHudProviderService>().To<HudProviderService>().AsSingle();
            Container.Bind<IUiMessagesService>().To<UiMessagesService>().AsSingle();
            Container.Bind<ICustomersDeliveringService>().To<CustomersDeliveringService>().AsSingle();
            Container.Bind<ICustomersPoolingService>().To<CustomersPoolingService>().AsSingle();
            Container.Bind<IDaysService>().To<DaysService>().AsSingle();
            Container.Bind<ISkillService>().To<SkillService>().AsSingle();
            Container.Bind<IPlayerLivesService>().To<PlayerLivesService>().AsSingle();
            Container.Bind<ICustomersRegistryService>().To<CustomersRegistryService>().AsSingle();
        }

        private void InstallFactories()
        {
            Container.Bind<ICharactersFactory>().To<CharactersFactory>().AsSingle();
            Container.Bind<IInteractablesFactory>().To<InteractablesFactory>().AsSingle();
            Container.Bind<IHudFactory>().To<HudFactory>().AsSingle();
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
            Container.Bind<MorningState>().AsSingle();
            Container.Bind<DayState>().AsSingle();
            Container.Bind<GameOverState>().AsSingle();
        }
    }
}