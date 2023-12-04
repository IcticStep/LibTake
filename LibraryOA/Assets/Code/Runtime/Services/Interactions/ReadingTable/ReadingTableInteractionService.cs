using System;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Interactions.Data;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.Services.Player;
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

        public bool CanInteract(IBookStorage bookStorage, Progress progress) =>
            CanRead(bookStorage, progress);

        public void Interact(IBookStorage bookStorage, Progress progress)
        {
            if(!bookStorage.HasBook && !progress.Empty)
            {
                progress.Reset();
                return;
            }

            StartReadingIfPossible(bookStorage, progress);
        }

        public void StartReadingIfPossible(IBookStorage bookStorage, Progress progress)
        {
            if(CanRead(bookStorage, progress))
                progress.StartFilling(GetOnProgressFinishCallback(bookStorage));
        }

        public void StopReading(Progress progress) =>
            progress.StopFilling();

        private bool CanRead(IBookStorage bookStorage, Progress progress) =>
            bookStorage.HasBook
            && !_playerInventoryService.HasBook
            && progress.CanBeStarted 
            && _readBookService.CanReadBook(bookStorage.CurrentBookId);

        private Action GetOnProgressFinishCallback(IBookStorage bookStorage) =>
            () => _readBookService.ReadBook(bookStorage.CurrentBookId);
    }
}