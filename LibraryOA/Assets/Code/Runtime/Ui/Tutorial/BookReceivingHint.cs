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
    internal sealed class BookReceivingHint : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        
        private IPersistantProgressService _persistantProgressService;
        private IPlayerInventoryService _playerInventoryService;
        private IBooksReceivingInteractionsService _booksReceivingInteractionsService;
        private bool _showStarted;

        private TutorialData ProgressTutorialData => _persistantProgressService.Progress.TutorialData;

        [Inject]
        private void Construct(IPersistantProgressService persistantProgressService, IPlayerInventoryService playerInventoryService,
            IBooksReceivingInteractionsService booksReceivingInteractionsService)
        {
            _booksReceivingInteractionsService = booksReceivingInteractionsService;
            _playerInventoryService = playerInventoryService;
            _persistantProgressService = persistantProgressService;
        }

        private void Awake()
        {
            _playerInventoryService.AllBooksRemoved += OnInventoryBecameEmpty;
            _booksReceivingInteractionsService.BookReceived += OnFirstBookReceived;
        }

        private void Start() =>
            _smoothFader.FadeImmediately();

        private void OnDestroy()
        {
            _playerInventoryService.AllBooksRemoved -= OnInventoryBecameEmpty;
            _booksReceivingInteractionsService.BookReceived -= OnFirstBookReceived;
        }

        private void OnInventoryBecameEmpty()
        {
            if(ProgressTutorialData.BookReceivingHintShown || _showStarted)
                return;
            
            _showStarted = true;
            _smoothFader.UnFade();
        }

        private void OnFirstBookReceived()
        {
            if(_smoothFader.IsFullyInvisible)
                return;

            _smoothFader.Fade();
            ProgressTutorialData.BookReceivingHintShown = true;
        }
    }
}