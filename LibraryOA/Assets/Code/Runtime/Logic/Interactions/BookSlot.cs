using Code.Runtime.Logic.Interactions.Api;
using Code.Runtime.Logic.Interactions.Data;
using Code.Runtime.Services.Interactions.BookSlotInteraction;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    internal sealed class BookSlot : Interactable, IHoverStartListener, IHoverEndListener
    {
        [SerializeField] 
        private BookStorageHolder _bookStorageObject;
        [SerializeField]
        private BookStorageDataView _bookStorageDataView;
        
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

        public void OnHoverStart() =>
            _bookStorageDataView.ShowData();

        public void OnHoverEnd() =>
            _bookStorageDataView.HideData();
    }
}