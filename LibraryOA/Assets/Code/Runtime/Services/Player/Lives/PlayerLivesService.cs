using System;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.States;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Player.Lives
{
    [UsedImplicitly]
    internal sealed class PlayerLivesService : IPlayerLivesService
    {
        private readonly GameStateMachine _gameStateMachine;
        public int Health { get; private set; }

        public event Action Updated;

        public PlayerLivesService(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void WasteLife()
        {
            if(Health <= 0)
                return;
            
            Health--;
            Updated?.Invoke();
            FinishGame();
        }

        public void RestoreLife()
        {
            Health++;
            Updated?.Invoke();
        }

        public void LoadProgress(Progress progress) =>
            Health = progress.PlayerData.Health;

        public void UpdateProgress(Progress progress) =>
            progress.PlayerData.Health = Health;

        private void FinishGame()
        {
            if(_gameStateMachine.ActiveStateType == typeof(DayState))
                _gameStateMachine.EnterState<>();
        }
    }
}