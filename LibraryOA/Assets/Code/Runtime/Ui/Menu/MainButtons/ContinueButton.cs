using Code.Runtime.Data;
using Code.Runtime.Infrastructure.GameStates;
using Code.Runtime.Infrastructure.GameStates.States;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Services.Loading;
using Code.Runtime.Ui.Menu.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui.Menu.MainButtons
{
    internal sealed class ContinueButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private MenuGroup _mainButtonsGroup;
        [SerializeField]
        private GameName _gameName;
        
        private GameStateMachine _gameStateMachine;
        private ISaveLoadService _saveLoadService;
        private ILoadingCurtainService _loadingCurtainService;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService, ILoadingCurtainService loadingCurtainService)
        {
            _loadingCurtainService = loadingCurtainService;
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnContinueButtonPressed);
            _saveLoadService.Updated += UpdateInteractableState;
        }

        private void Start() =>
            UpdateInteractableState();

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnContinueButtonPressed);
            _saveLoadService.Updated -= UpdateInteractableState;
        }

        private void UpdateInteractableState() =>
            _button.interactable = _saveLoadService.HasSavedProgress;

        private void OnContinueButtonPressed() =>
            ContinueGame()
                .Forget();

        private async UniTaskVoid ContinueGame()
        {
            await UniTask.WhenAll(_mainButtonsGroup.Hide(), _gameName.Hide(), _loadingCurtainService.ShowImageAsync());
            _gameStateMachine.EnterState<LoadProgressState, LoadProgressOption>(LoadProgressOption.LoadProgressIfAny);
        }
    }
}