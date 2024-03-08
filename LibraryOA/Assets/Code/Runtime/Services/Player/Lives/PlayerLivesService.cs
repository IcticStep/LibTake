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
        public int StartLives => _staticDataService.Player.StartLivesCount;
        public int RestoreLifePrice => _staticDataService.Interactables.Statue.LifeRestorePrice;
        
        public event Action Updated;
        public event Action RestoredLife;

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
            RestoredLife?.Invoke();
        }

        public void LoadProgress(GameProgress progress) =>
            Lives = progress.PlayerData.Lives;

        public void UpdateProgress(GameProgress progress) =>
            progress.PlayerData.Lives = Lives;
    }
}