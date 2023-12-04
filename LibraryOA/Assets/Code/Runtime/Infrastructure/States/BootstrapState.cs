using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Services.Days;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.Services.Player;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ISaveLoadRegistry _saveLoadRegistry;
        private readonly IDaysService _daysService;
        private readonly IPlayerInventoryService _playerInventoryService;
        private readonly IReadBookService _readBookService;

        public BootstrapState(GameStateMachine stateMachine, ISceneLoader sceneLoader, ISaveLoadRegistry saveLoadRegistry,
            IDaysService daysService, IPlayerInventoryService playerInventoryService, IReadBookService readBookService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _saveLoadRegistry = saveLoadRegistry;
            _daysService = daysService;
            _playerInventoryService = playerInventoryService;
            _readBookService = readBookService;
        }

        public void Start()
        {
            RegisterServicesAsSavedProgress();
            _sceneLoader.LoadSceneAsync(InitialSceneName, OnInitSceneLoaded).Forget();
        }

        public void Exit()
        {

        }

        private void OnInitSceneLoaded() =>
            _stateMachine.EnterState<WarmupState>();

        private void RegisterServicesAsSavedProgress()
        {
            _saveLoadRegistry.Register(_daysService);
            _saveLoadRegistry.Register(_playerInventoryService);
            _saveLoadRegistry.Register(_readBookService);
        }
    }
}