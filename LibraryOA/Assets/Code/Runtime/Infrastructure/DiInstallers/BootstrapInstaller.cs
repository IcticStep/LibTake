using System;
using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Infrastructure.GameStates;
using Code.Runtime.Infrastructure.GameStates.Factories;
using Code.Runtime.Infrastructure.GameStates.States;
using Code.Runtime.Infrastructure.Services.Camera;
using Code.Runtime.Infrastructure.Services.CleanUp;
using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Infrastructure.Services.Locales;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.Services.UiHud;
using Code.Runtime.Infrastructure.Services.UiMessages;
using Code.Runtime.Logic.Audio;
using Code.Runtime.Services.Books.Delivering;
using Code.Runtime.Services.Books.Receiving;
using Code.Runtime.Services.Books.Reward;
using Code.Runtime.Services.Customers.Delivering;
using Code.Runtime.Services.Customers.Pooling;
using Code.Runtime.Services.Customers.Queue;
using Code.Runtime.Services.Customers.Registry;
using Code.Runtime.Services.Days;
using Code.Runtime.Services.GlobalGoals;
using Code.Runtime.Services.GlobalGoals.Presenter;
using Code.Runtime.Services.GlobalGoals.Visualization;
using Code.Runtime.Services.GlobalRocket;
using Code.Runtime.Services.InputService;
using Code.Runtime.Services.Interactions.BookSlotInteraction;
using Code.Runtime.Services.Interactions.BooksReceiving;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.Services.Interactions.ReadingTable;
using Code.Runtime.Services.Interactions.Registry;
using Code.Runtime.Services.Interactions.ScannerInteraction;
using Code.Runtime.Services.Interactions.Scanning;
using Code.Runtime.Services.Interactions.Statue;
using Code.Runtime.Services.Interactions.Truck;
using Code.Runtime.Services.Library;
using Code.Runtime.Services.Loading;
using Code.Runtime.Services.Pause;
using Code.Runtime.Services.Physics;
using Code.Runtime.Services.Player.CutsceneCopyProvider;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Services.Player.Lives;
using Code.Runtime.Services.Player.Provider;
using Code.Runtime.Services.Random;
using Code.Runtime.Services.Skills;
using Code.Runtime.Services.TruckDriving;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using SettingsService = Code.Runtime.Infrastructure.Settings.SettingsService;

namespace Code.Runtime.Infrastructure.DiInstallers
{
    internal sealed class BootstrapInstaller : MonoInstaller, IInitializable
    {
        [SerializeField]
        private AudioPlayer _audioPlayer;
        
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
            Container.Resolve<GameStateMachine>().EnterState<BootstrapGameState>();
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
            Container.Bind(typeof(ILocalizationService), typeof(IDisposable)).To<LocalizationService>().AsSingle();
            Container.Bind<ILoadingCurtainService>().To<LoadingCurtainService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SettingsService>().AsSingle();
            Container.BindInstance(_audioPlayer).AsSingle();
        }

        private void InstallServices()
        {
            InstallWrappers();
            InstallPlayerServices();
            InstallInteractionsServices();
            InstallLevelServices();
            InstallUiServices();
            InstallGlobalGoalsServices();
            InstallGameplayServices();
        }

        private void InstallWrappers()
        {
            Container.Bind<IPhysicsService>().To<PhysicsService>().AsSingle();
            Container.Bind<IRandomService>().To<RandomService>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
        }

        private void InstallPlayerServices()
        {
            Container.Bind<IPlayerProviderService>().To<PlayerProviderService>().AsSingle();
            Container.Bind<IPlayerInventoryService>().To<PlayerInventoryService>().AsSingle();
            Container.Bind<IPlayerLivesService>().To<PlayerLivesService>().AsSingle();
            Container.Bind<IPlayerSkillService>().To<PlayerSkillService>().AsSingle();
            Container.Bind<IPlayerCutsceneCopyProvider>().To<PlayerCutsceneCopyProvider>().AsSingle();
        }

        private void InstallInteractionsServices()
        {
            Container.Bind<IInteractablesRegistry>().To<InteractablesRegistry>().AsSingle();
            Container.Bind<IBookSlotInteractionService>().To<BookSlotInteractionService>().AsSingle();
            Container.Bind<IReadingTableInteractionService>().To<ReadingTableInteractionService>().AsSingle();
            Container.Bind<ITruckInteractionService>().To<TruckInteractionService>().AsSingle();
            Container.Bind<IBooksReceivingInteractionsService>().To<BooksReceivingInteractionsService>().AsSingle();
            Container.Bind<IScannerInteractionService>().To<ScannerInteractionService>().AsSingle();
            Container.Bind<IStatueInteractionService>().To<StatueInteractionService>().AsSingle();
        }

        private void InstallLevelServices()
        {
            Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle();
            Container.Bind<ITruckProvider>().To<TruckProvider>().AsSingle();
            Container.Bind<ILibraryService>().To<LibraryService>().AsSingle();
            Container.Bind<IRocketProvider>().To<RocketProvider>().AsSingle();
            Container.Bind<IPauseService>().To<PauseService>().AsSingle();
        }

        private void InstallUiServices()
        {
            Container.Bind<IHudProviderService>().To<HudService>().AsSingle();
            Container.Bind<IUiMessagesService>().To<UiMessagesService>().AsSingle();
        }

        private void InstallGlobalGoalsServices()
        {
            Container.Bind<IGlobalGoalsVisualizationService>().To<GlobalGoalsVisualizationService>().AsSingle();
            Container.Bind<IGlobalGoalService>().To<GlobalGoalService>().AsSingle();
            Container.Bind<IGlobalGoalPresenterService>().To<GlobalGoalPresenterService>().AsSingle();
        }

        private void InstallGameplayServices()
        {
            Container.Bind<IDaysService>().To<DaysService>().AsSingle();
            Container.Bind<IReadBookService>().To<ReadBookService>().AsSingle();
            Container.Bind<IBooksDeliveringService>().To<BooksDeliveringService>().AsSingle();
            Container.Bind<ICustomersQueueService>().To<CustomersQueueService>().AsSingle();
            Container.Bind<IBooksReceivingService>().To<BooksReceivingService>().AsSingle();
            Container.Bind<ICustomersDeliveringService>().To<CustomersDeliveringService>().AsSingle();
            Container.Bind<ICustomersPoolingService>().To<CustomersPoolingService>().AsSingle();
            Container.Bind<ICustomersRegistryService>().To<CustomersRegistryService>().AsSingle();
            Container.Bind<IBookRewardService>().To<BookRewardService>().AsSingle();
            Container.Bind<IScanBookService>().To<ScanBookService>().AsSingle();
            Container.Bind<ICraftingService>().To<CraftingService>().AsSingle();
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
            Container.Bind<BootstrapGameState>().AsSingle();
            Container.Bind<WarmupGameState>().AsSingle();
            Container.Bind<MenuGameState>().AsSingle();
            Container.Bind<LoadProgressState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<MorningGameState>().AsSingle();
            Container.Bind<DayGameState>().AsSingle();
            Container.Bind<GameOverGameState>().AsSingle();
            Container.Bind<RestartGameState>().AsSingle();
            Container.Bind<FinishGlobalGoalState>().AsSingle();
        }
    }
}