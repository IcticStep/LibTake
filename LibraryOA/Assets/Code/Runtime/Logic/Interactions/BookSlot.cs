using Code.Runtime.Logic.Interactions.Api;
using Code.Runtime.Services.Interactions.BookSlotInteraction;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    internal sealed class BookSlot : Interactable, IHoverStartListener, IHoverEndListener
    {
        [SerializeField] 
        private BookStorage _bookStorageObject;
        [SerializeField]
        private BookStorageDataView _bookStorageDataView;
        
        private IBookSlotInteractionService _bookSlotInteractionService;

        [Inject]
        private void Construct(IBookSlotInteractionService bookSlotInteractionService) =>
            _bookSlotInteractionService = bookSlotInteractionService;
        
        public override bool CanInteract() =>
            _bookSlotInteractionService.CanInteract(_bookStorageObject);

        public override void Interact() =>
            _bookSlotInteractionService.Interact(_bookStorageObject);

        public void OnHoverStart() =>
            _bookStorageDataView.ShowData();

        public void OnHoverEnd() =>
            _bookStorageDataView.HideData();
    }
}