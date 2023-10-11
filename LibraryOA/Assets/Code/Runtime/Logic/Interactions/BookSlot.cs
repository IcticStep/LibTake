using Code.Runtime.Services.Interactions;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    internal sealed class BookSlot : Interactable
    {
        [SerializeField] private BookStorage _bookStorage;
        private IBookSlotInteractService _bookSlotInteractService;

        [Inject]
        private void Construct(IBookSlotInteractService bookSlotInteractService) =>
            _bookSlotInteractService = bookSlotInteractService;
        
        public override bool CanInteract() =>
            _bookSlotInteractService.CanInteract(_bookStorage);

        public override void Interact() =>
            _bookSlotInteractService.Interact(_bookStorage);
    }
}