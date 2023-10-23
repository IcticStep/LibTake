using Code.Runtime.Data;
using Code.Runtime.Logic.Interactions.Data;
using Code.Runtime.Services.Interactions;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    internal sealed class ReadingTable : Interactable
    {
        [SerializeField] 
        private BookStorageHolder _bookStorageObject;
        
        private IReadingTableInteractionService _readingTableInteractionService;
        private IBookStorage _bookStorage;

        private void Start() =>
            _bookStorage = _bookStorageObject.BookStorage;

        [Inject]
        private void Construct(IReadingTableInteractionService readingTableInteractionService) =>
            _readingTableInteractionService = readingTableInteractionService;
        
        public override bool CanInteract() =>
            _readingTableInteractionService.CanInteract(_bookStorage);

        public override void Interact() =>
            _readingTableInteractionService.Interact(_bookStorage);
    }
}