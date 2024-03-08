using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Services.Interactions.BooksReceiving;
using Code.Runtime.Services.Interactions.Truck;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Ui.Common;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.Tutorial
{
    internal sealed class BookStorageHint : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        
        private IPersistantProgressService _persistantProgressService;
        private ITruckInteractionService _truckInteractionService;
        private IPlayerInventoryService _playerInventoryService;

        private TutorialData ProgressTutorialData => _persistantProgressService.Progress.TutorialData;

        [Inject]
        private void Construct(IPersistantProgressService persistantProgressService, IBooksReceivingInteractionsService booksReceivingInteractionsService,
            ITruckInteractionService truckInteractionService, IPlayerInventoryService playerInventoryService)
        {
            _playerInventoryService = playerInventoryService;
            _truckInteractionService = truckInteractionService;
            _persistantProgressService = persistantProgressService;
        }

        private void Awake()
        {
            _playerInventoryService.AllBooksRemoved += OnInventoryBecameEmpty;
            _truckInteractionService.BooksTaken += OnBooksFromTruckTaken;
        }

        private void Start() =>
            _smoothFader.FadeImmediately();

        private void OnDestroy()
        {
            _playerInventoryService.AllBooksRemoved -= OnInventoryBecameEmpty;
            _truckInteractionService.BooksTaken -= OnBooksFromTruckTaken;
        }

        private void OnInventoryBecameEmpty()
        {
            if(!_smoothFader.IsFullyVisible)
                return;

            _smoothFader.Fade();
            ProgressTutorialData.BookStorageHintShown = true;
        }

        private void OnBooksFromTruckTaken()
        {
            if(ProgressTutorialData.BookStorageHintShown)
                return;
            
            _smoothFader.UnFade();
        }
    }
}