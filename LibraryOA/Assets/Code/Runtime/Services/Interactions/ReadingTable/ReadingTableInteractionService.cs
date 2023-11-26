using System;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Logic.Interactions.Data;
using Code.Runtime.Services.Interactions.BookSlotInteraction;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.Services.Player;
using JetBrains.Annotations;
using Progress = Code.Runtime.Logic.Progress;

namespace Code.Runtime.Services.Interactions.ReadingTable
{
    [UsedImplicitly]
    internal sealed class ReadingTableInteractionService : IReadingTableInteractionService
    {
        private readonly IPlayerProgressService _playerProgressService;
        private readonly IReadBookService _readBookService;
        private readonly IPlayerInventoryService _playerInventoryService;

        public ReadingTableInteractionService(IBookSlotInteractionService bookSlotInteractionService, IPlayerProgressService playerProgressService,
            IReadBookService readBookService, IPlayerInventoryService playerInventoryService)
        {
            _playerProgressService = playerProgressService;
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
            && !BookIsRead(bookStorage.CurrentBookId);

        private bool BookIsRead(string bookStorage) =>
            _playerProgressService.Progress.PlayerData.ReadBooks.IsBookRead(bookStorage);

        private Action GetOnProgressFinishCallback(IBookStorage bookStorage) =>
            () => _readBookService.ReadBook(bookStorage.CurrentBookId);
    }
}