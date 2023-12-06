using System;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.States;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Player.Lives
{
    [UsedImplicitly]
    internal sealed class PlayerLivesService : IPlayerLivesService
    {
        private readonly GameStateMachine _gameStateMachine;
        public int Lives { get; private set; }

        public event Action Updated;

        public PlayerLivesService(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void WasteLife()
        {
            if(Lives <= 0)
                return;
            
            Lives--;
            Debug.Log($"Lives count: {Lives}.");
            Updated?.Invoke();
            
            if(Lives <= 0)
                FinishGame();
        }

        public void RestoreLife()
        {
            Lives++;
            Debug.Log($"Lives count: {Lives}.");
            Updated?.Invoke();
        }

        public void LoadProgress(Progress progress) =>
            Lives = progress.PlayerData.Lives;

        public void UpdateProgress(Progress progress) =>
            progress.PlayerData.Lives = Lives;

        private void FinishGame()
        {
            if(_gameStateMachine.ActiveStateType == typeof(DayState))
                _gameStateMachine.EnterState<GameOverState>();
        }
    }
}