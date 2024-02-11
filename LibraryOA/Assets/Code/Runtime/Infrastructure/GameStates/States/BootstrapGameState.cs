using Code.Runtime.Infrastructure.GameStates.Api;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Days;
using Code.Runtime.Services.GlobalGoals;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.Services.Interactions.Scanning;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Services.Player.Lives;
using Code.Runtime.Services.Skills;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.GameStates.States
{
    internal sealed class BootstrapGameState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ISaveLoadRegistry _saveLoadRegistry;
        private readonly IDaysService _daysService;
        private readonly IPlayerInventoryService _playerInventoryService;
        private readonly IReadBookService _readBookService;
        private readonly IPlayerSkillService _playerSkillService;
        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerLivesService _playerLivesService;
        private readonly IScanBookService _scanBookService;
        private readonly ICraftingService _craftingService;
        private readonly IGlobalGoalService _globalGoalService;

        public BootstrapGameState(GameStateMachine stateMachine, ISceneLoader sceneLoader, ISaveLoadRegistry saveLoadRegistry,
            IDaysService daysService, IPlayerInventoryService playerInventoryService, IReadBookService readBookService,
            IPlayerSkillService playerSkillService, IStaticDataService staticDataService, IPlayerLivesService playerLivesService,
            IScanBookService scanBookService, ICraftingService craftingService, IGlobalGoalService globalGoalService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _saveLoadRegistry = saveLoadRegistry;
            _daysService = daysService;
            _playerInventoryService = playerInventoryService;
            _readBookService = readBookService;
            _playerSkillService = playerSkillService;
            _staticDataService = staticDataService;
            _playerLivesService = playerLivesService;
            _scanBookService = scanBookService;
            _craftingService = craftingService;
            _globalGoalService = globalGoalService;
        }

        public void Start()
        {
            _staticDataService.LoadStartupSettings();
            RegisterServicesAsSavedProgress();

            string bootstrapSceneName = _staticDataService.ScenesRouting.BootstrapScene;
            _sceneLoader.LoadSceneAsync(bootstrapSceneName, OnInitSceneLoaded).Forget();
        }

        public void Exit() { }

        private void OnInitSceneLoaded() =>
            _stateMachine.EnterState<WarmupGameState>();

        private void RegisterServicesAsSavedProgress()
        {
            _saveLoadRegistry.Register(_daysService);
            _saveLoadRegistry.Register(_playerInventoryService);
            _saveLoadRegistry.Register(_readBookService);
            _saveLoadRegistry.Register(_playerSkillService);
            _saveLoadRegistry.Register(_playerLivesService);
            _saveLoadRegistry.Register(_scanBookService);
            _saveLoadRegistry.Register(_craftingService);
            _saveLoadRegistry.Register(_globalGoalService);
        }
    }
}