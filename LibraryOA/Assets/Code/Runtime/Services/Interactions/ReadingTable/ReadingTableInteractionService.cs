using System;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Interactables.Data;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.Services.Player.Inventory;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Interactions.ReadingTable
{
    [UsedImplicitly]
    internal sealed class ReadingTableInteractionService : IReadingTableInteractionService
    {
        private readonly IReadBookService _readBookService;
        private readonly IPlayerInventoryService _playerInventoryService;

        public ReadingTableInteractionService(IReadBookService readBookService, IPlayerInventoryService playerInventoryService)
        {
            _readBookService = readBookService;
            _playerInventoryService = playerInventoryService;
        }

        public bool CanInteract(IBookStorage bookStorage, IProgress progress) =>
            CanRead(bookStorage, progress);

        public void Interact(IBookStorage bookStorage, IProgress progress)
        {
            if(!bookStorage.HasBook && !progress.Empty)
            {
                progress.Reset();
                return;
            }

            StartReadingIfPossible(bookStorage, progress);
        }

        public void StartReadingIfPossible(IBookStorage bookStorage, IProgress progress)
        {
            if(CanRead(bookStorage, progress))
                progress.StartFilling(GetOnProgressFinishCallback(bookStorage));
        }

        public void StopReading(IProgress progress) =>
            progress.StopFilling();

        private bool CanRead(IBookStorage bookStorage, IProgress progress) =>
            bookStorage.HasBook
            && !_playerInventoryService.HasBook
            && progress.CanBeStarted 
            && _readBookService.CanReadBook(bookStorage.CurrentBookId);

        private Action GetOnProgressFinishCallback(IBookStorage bookStorage) =>
            () => _readBookService.ReadBook(bookStorage.CurrentBookId);
    }
}