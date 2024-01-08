using System;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.StaticData;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Player.Lives
{
    [UsedImplicitly]
    public sealed class PlayerLivesService : IPlayerLivesService
    {
        private readonly IStaticDataService _staticDataService;

        public int Lives { get; private set; }
        public int MaxLives => _staticDataService.Player.MaxLivesCount;
        public event Action Updated;

        public PlayerLivesService(IStaticDataService staticDataService) 
        {
            _staticDataService = staticDataService;
        }

        public void WasteLife()
        {
            if(Lives <= 0)
                return;
            
            Lives--;
            Debug.Log($"Lives count: {Lives}.");
            Updated?.Invoke();
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
    }
}