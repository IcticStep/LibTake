using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Interactions.BooksReceiving;
using Code.Runtime.Services.Interactions.Truck;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Services.Player.Lives;
using Code.Runtime.Ui.Common;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.Tutorial
{
    internal sealed class BuyHealthHint : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        
        private IPersistantProgressService _persistantProgressService;
        private IPlayerInventoryService _playerInventoryService;
        private bool _showStarted;
        private IPlayerLivesService _playerLivesService;

        private TutorialData ProgressTutorialData => _persistantProgressService.Progress.TutorialData;

        [Inject]
        private void Construct(IPersistantProgressService persistantProgressService, IPlayerInventoryService playerInventoryService,
            IPlayerLivesService playerLivesService)
        {
            _playerLivesService = playerLivesService;
            _playerInventoryService = playerInventoryService;
            _persistantProgressService = persistantProgressService;
        }

        private void Start()
        {
            _playerInventoryService.CoinsUpdated += OnCoinsUpdated;
            _playerLivesService.RestoredLife += OnLiveRestored;
            
            if(TryShowHint())
                return;
            _smoothFader.FadeImmediately();
        }

        private void OnDestroy()
        {
            _playerInventoryService.AllBooksRemoved -= OnCoinsUpdated;
            _playerLivesService.RestoredLife -= OnLiveRestored;
        }

        private void OnCoinsUpdated() =>
            TryShowHint();

        private bool TryShowHint()
        {
            if(ProgressTutorialData.BuyHealthHintShown || _showStarted)
                return false;
            
            if(_playerLivesService.Lives >= _playerLivesService.StartLives || _playerInventoryService.Coins < _playerLivesService.RestoreLifePrice)
                return false;
            
            _showStarted = true;
            _smoothFader.UnFade();
            return true;
        }

        private void OnLiveRestored()
        {
            if(_smoothFader.IsFullyInvisible || !_showStarted)
                return;

            _smoothFader.Fade();
            ProgressTutorialData.BuyHealthHintShown = true;
        }
    }
}