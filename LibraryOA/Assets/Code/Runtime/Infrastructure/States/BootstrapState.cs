using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Services.Days;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.Services.Interactions.Scanning;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Services.Player.Lives;
using Code.Runtime.Services.Skills;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ISaveLoadRegistry _saveLoadRegistry;
        private readonly IDaysService _daysService;
        private readonly IPlayerInventoryService _playerInventoryService;
        private readonly IReadBookService _readBookService;
        private readonly ISkillService _skillService;
        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerLivesService _playerLivesService;
        private readonly IScanBookService _scanBookService;

        public BootstrapState(GameStateMachine stateMachine, ISceneLoader sceneLoader, ISaveLoadRegistry saveLoadRegistry,
            IDaysService daysService, IPlayerInventoryService playerInventoryService, IReadBookService readBookService,
            ISkillService skillService, IStaticDataService staticDataService, IPlayerLivesService playerLivesService,
            IScanBookService scanBookService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _saveLoadRegistry = saveLoadRegistry;
            _daysService = daysService;
            _playerInventoryService = playerInventoryService;
            _readBookService = readBookService;
            _skillService = skillService;
            _staticDataService = staticDataService;
            _playerLivesService = playerLivesService;
            _scanBookService = scanBookService;
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
            _stateMachine.EnterState<WarmupState>();

        private void RegisterServicesAsSavedProgress()
        {
            _saveLoadRegistry.Register(_daysService);
            _saveLoadRegistry.Register(_playerInventoryService);
            _saveLoadRegistry.Register(_readBookService);
            _saveLoadRegistry.Register(_skillService);
            _saveLoadRegistry.Register(_playerLivesService);
            _saveLoadRegistry.Register(_scanBookService);
        }
    }
}