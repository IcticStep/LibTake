using Code.Runtime.Data;
using Code.Runtime.Infrastructure.GameStates;
using Code.Runtime.Infrastructure.GameStates.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui.Menu.MainButtons
{
    internal sealed class NewGameButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        private void Awake() =>
            _button.onClick.AddListener(StartNewGame);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(StartNewGame);

        private void StartNewGame() =>
            _gameStateMachine.EnterState<LoadProgressState, LoadProgressOption>(LoadProgressOption.ForceCreatingNewProgress);
    }
}