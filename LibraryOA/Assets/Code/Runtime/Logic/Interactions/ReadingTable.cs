using Code.Runtime.Logic.Interactions.Data;
using Code.Runtime.Services.Interactions;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    internal sealed class ReadingTable : Interactable, IHoverStartListener, IHoverEndListener
    {
        [SerializeField] 
        private BookStorageHolder _bookStorageObject;
        [SerializeField]
        private Progress _progress;
        
        private IReadingTableInteractionService _readingTableInteractionService;
        private IBookStorage _bookStorage;
        private IBookSlotInteractionService _bookSlotInteractionService;

        private void Start() =>
            _bookStorage = _bookStorageObject.BookStorage;

        [Inject]
        private void Construct(IReadingTableInteractionService readingTableInteractionService, IBookSlotInteractionService bookSlotInteractionService)
        {
            _readingTableInteractionService = readingTableInteractionService;
            _bookSlotInteractionService = bookSlotInteractionService;
        }

        public override bool CanInteract() =>
            _bookSlotInteractionService.CanInteract(_bookStorage);

        public override void Interact()
        {
            _bookSlotInteractionService.Interact(_bookStorage);
            _readingTableInteractionService.Interact(_bookStorage, _progress);
        }

        public void OnHoverStart() =>
            _readingTableInteractionService.StartReadingIfPossible(_bookStorage, _progress);

        public void OnHoverEnd() =>
            _readingTableInteractionService.StopReading(_progress);
    }
}