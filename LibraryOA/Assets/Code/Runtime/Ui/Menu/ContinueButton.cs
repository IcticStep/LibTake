using Code.Runtime.Data;
using Code.Runtime.Infrastructure.GameStates;
using Code.Runtime.Infrastructure.GameStates.States;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui.Menu
{
    internal sealed class ContinueButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        
        private GameStateMachine _gameStateMachine;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _button.onClick.AddListener(StartNewGame);
            _saveLoadService.Updated += UpdateInteractableState;
        }

        private void Start() =>
            UpdateInteractableState();

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(StartNewGame);
            _saveLoadService.Updated -= UpdateInteractableState;
        }

        private void UpdateInteractableState() =>
            _button.interactable = _saveLoadService.HasSavedProgress;

        private void StartNewGame() =>
            _gameStateMachine.EnterState<LoadProgressState, LoadProgressOption>(LoadProgressOption.LoadProgressIfAny);
    }
}