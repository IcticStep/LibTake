using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.StaticData.Books;
using Code.Runtime.Ui.Common;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.Tutorial
{
    internal sealed class ReadBookHint : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        
        private IPersistantProgressService _persistantProgressService;
        private IPlayerInventoryService _playerInventoryService;
        private bool _showStarted;
        private IReadBookService _readBookService;

        private TutorialData ProgressTutorialData => _persistantProgressService.Progress.TutorialData;

        [Inject]
        private void Construct(IPersistantProgressService persistantProgressService, IPlayerInventoryService playerInventoryService,
            IReadBookService readBookService)
        {
            _readBookService = readBookService;
            _playerInventoryService = playerInventoryService;
            _persistantProgressService = persistantProgressService;
        }

        private void Awake()
        {
            _playerInventoryService.AllBooksRemoved += OnInventoryBecameEmpty;
            _readBookService.BookRead += OnBookRead;
        }

        private void Start() =>
            _smoothFader.FadeImmediately();

        private void OnDestroy()
        {
            _playerInventoryService.AllBooksRemoved -= OnInventoryBecameEmpty;
            _readBookService.BookRead -= OnBookRead;
        }

        private void OnInventoryBecameEmpty()
        {
            if(ProgressTutorialData.ReadBookHintShown || _showStarted)
                return;
            
            _showStarted = true;
            _smoothFader.UnFade();
        }

        private void OnBookRead(StaticBook staticBook)
        {
            if(_smoothFader.IsFullyInvisible)
                return;

            _smoothFader.Fade();
            ProgressTutorialData.ReadBookHintShown = true;
        }
    }
}