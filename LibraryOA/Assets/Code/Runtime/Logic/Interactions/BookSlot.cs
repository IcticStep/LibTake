using Code.Runtime.Data;
using Code.Runtime.Logic.Interactions.Data;
using Code.Runtime.Services.Interactions;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    internal sealed class BookSlot : Interactable
    {
        [SerializeField] 
        private BookStorageHolder _bookStorageObject;
        
        private IBookSlotInteractionService _bookSlotInteractionService;
        private IBookStorage _bookStorage;

        private void Start() =>
            _bookStorage = _bookStorageObject.BookStorage;

        [Inject]
        private void Construct(IBookSlotInteractionService bookSlotInteractionService) =>
            _bookSlotInteractionService = bookSlotInteractionService;
        
        public override bool CanInteract() =>
            _bookSlotInteractionService.CanInteract(_bookStorage);

        public override void Interact() =>
            _bookSlotInteractionService.Interact(_bookStorage);
    }
}