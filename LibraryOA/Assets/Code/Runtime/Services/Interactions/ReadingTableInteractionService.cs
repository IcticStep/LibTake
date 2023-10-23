using System;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Logic.Interactions.Data;
using JetBrains.Annotations;
using Progress = Code.Runtime.Logic.Progress;

namespace Code.Runtime.Services.Interactions
{
    [UsedImplicitly]
    internal sealed class ReadingTableInteractionService : IReadingTableInteractionService
    {
        private readonly IBookSlotInteractionService _bookSlotInteractionService;
        private readonly IPlayerProgressService _playerProgressService;
        private readonly IReadBookService _readBookService;

        public ReadingTableInteractionService(IBookSlotInteractionService bookSlotInteractionService, IPlayerProgressService playerProgressService,
            IReadBookService readBookService)
        {
            _bookSlotInteractionService = bookSlotInteractionService;
            _playerProgressService = playerProgressService;
            _readBookService = readBookService;
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
            && progress.CanBeStarted 
            && !BookIsRead(bookStorage.CurrentBookId);

        private bool BookIsRead(string bookStorage) =>
            _playerProgressService.Progress.PlayerData.ReadBooks.IsBookRead(bookStorage);

        private Action GetOnProgressFinishCallback(IBookStorage bookStorage) =>
            () => _readBookService.ReadBook(bookStorage.CurrentBookId);
    }
}