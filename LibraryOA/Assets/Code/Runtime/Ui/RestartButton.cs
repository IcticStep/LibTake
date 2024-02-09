using Code.Runtime.Infrastructure.GameStates;
using Code.Runtime.Infrastructure.GameStates.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui
{
    internal sealed class RestartButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        private void Awake() =>
            _button.onClick.AddListener(Restart);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(Restart);

        private void Restart()
        {
            _button.interactable = false;
            _gameStateMachine.EnterState<RestartGameState>();
        }
    }
}