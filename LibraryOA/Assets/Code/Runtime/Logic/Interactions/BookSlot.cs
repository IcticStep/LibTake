using System;
using Code.Runtime.Data;
using Code.Runtime.Services.Interactions;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    internal sealed class BookSlot : Interactable
    {
        [SerializeField] private BookStorageHolder _bookStorageObject;
        
        private IBookSlotInteractService _bookSlotInteractService;
        private IBookStorage _bookStorage;

        private void Start() =>
            _bookStorage = _bookStorageObject.BookStorage;

        [Inject]
        private void Construct(IBookSlotInteractService bookSlotInteractService) =>
            _bookSlotInteractService = bookSlotInteractService;
        
        public override bool CanInteract() =>
            _bookSlotInteractService.CanInteract(_bookStorage);

        public override void Interact() =>
            _bookSlotInteractService.Interact(_bookStorage);
    }
}