using System;
using Code.Runtime.Infrastructure.GameStates;
using Code.Runtime.Infrastructure.GameStates.States;
using Code.Runtime.Ui.Common;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui
{
    internal sealed class InteractablesSleepView : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        private void Awake() =>
            _gameStateMachine.StateChanged += OnGameStateChanged;

        private void Start() =>
            UpdateView();

        private void OnDestroy() =>
            _gameStateMachine.StateChanged -= OnGameStateChanged;

        private void OnGameStateChanged() =>
            UpdateView();

        private void UpdateView()
        {
            if(_gameStateMachine.ActiveStateType == typeof(MorningGameState))
                _smoothFader.UnFade();
            else
                _smoothFader.Fade();
        }
    }
}