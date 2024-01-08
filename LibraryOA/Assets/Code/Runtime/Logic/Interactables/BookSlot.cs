using Code.Runtime.Logic.Interactables.Api;
using Code.Runtime.Services.Interactions.BookSlotInteraction;
using Code.Runtime.Ui;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Logic.Interactables
{
    internal sealed class BookSlot : Interactable, IHoverStartListener, IHoverEndListener
    {
        [SerializeField] 
        private BookStorage _bookStorageObject;
        [FormerlySerializedAs("_bookStorageUi")]
        [FormerlySerializedAs("_bookStorageDataView")]
        [SerializeField]
        private BookUi _bookUi;
        
        private IBookSlotInteractionService _bookSlotInteractionService;

        [Inject]
        private void Construct(IBookSlotInteractionService bookSlotInteractionService) =>
            _bookSlotInteractionService = bookSlotInteractionService;
        
        public override bool CanInteract() =>
            _bookSlotInteractionService.CanInteract(_bookStorageObject);

        public override void Interact()
        {
            _bookSlotInteractionService.Interact(_bookStorageObject);
            _bookUi.ShowData();
        }

        public void OnHoverStart() =>
            _bookUi.ShowData();

        public void OnHoverEnd() =>
            _bookUi.HideData();
    }
}