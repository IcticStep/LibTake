using Code.Runtime.Data;
using Code.Runtime.Logic.Interactions.Data;
using Code.Runtime.Services.Interactions;
using TMPro;
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

        public override bool CanInteract()
        {
            Debug.Log("Can interact check.");
            return _bookSlotInteractionService.CanInteract(_bookStorage);
        }

        public override void Interact()
        {
            Debug.Log("Interact request.");
            _bookSlotInteractionService.Interact(_bookStorage);
            _readingTableInteractionService.Interact(_bookStorage, _progress);
        }

        public void OnHoverStart()
        {
            Debug.Log("Hovered");
            _readingTableInteractionService.StartReadingIfPossible(_bookStorage, _progress);
        }

        public void OnHoverEnd()
        {
            Debug.Log("Unhovered");
            _readingTableInteractionService.StopReading(_progress);
        }
    }
}