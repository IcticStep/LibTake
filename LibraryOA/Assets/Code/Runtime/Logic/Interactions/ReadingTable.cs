using Code.Runtime.Logic.Interactions.Api;
using Code.Runtime.Logic.Interactions.Data;
using Code.Runtime.Services.Interactions;
using Code.Runtime.Services.Interactions.BookSlotInteraction;
using Code.Runtime.Services.Interactions.ReadingTable;
using Code.Runtime.Ui;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    internal sealed class ReadingTable : Interactable, IHoverStartListener, IHoverEndListener, IProgressOwner
    {
        [SerializeField] 
        private BookStorage _bookStorageObject;
        [SerializeField]
        private Progress _progress;
        [FormerlySerializedAs("_bookStorageUi")]
        [FormerlySerializedAs("_bookStorageDataView")]
        [SerializeField]
        private BookUi _bookUi;
        
        private IReadingTableInteractionService _readingTableInteractionService;
        private IBookSlotInteractionService _bookSlotInteractionService;
        
        public bool InProgress => _progress.Running;

        [Inject]
        private void Construct(IReadingTableInteractionService readingTableInteractionService, IBookSlotInteractionService bookSlotInteractionService)
        {
            _readingTableInteractionService = readingTableInteractionService;
            _bookSlotInteractionService = bookSlotInteractionService;
        }

        public override bool CanInteract() =>
            _bookSlotInteractionService.CanInteract(_bookStorageObject);

        public override void Interact()
        {
            _bookSlotInteractionService.Interact(_bookStorageObject);
            _readingTableInteractionService.Interact(_bookStorageObject, _progress);
            
            if(_bookStorageObject.HasBook)
                _bookUi.ShowData();
            else
                _bookUi.HideData();
        }

        public void OnHoverStart()
        {
            _readingTableInteractionService.StartReadingIfPossible(_bookStorageObject, _progress);
            _bookUi.ShowData();
        }

        public void OnHoverEnd()
        {
            _readingTableInteractionService.StopReading(_progress);
            _bookUi.HideData();
        }
    }
}