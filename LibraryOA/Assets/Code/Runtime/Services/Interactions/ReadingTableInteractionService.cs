using System;
using System.Threading;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Logic.Interactions.Data;
using Code.Runtime.Player;
using Code.Runtime.Services.Player;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Unity.VisualScripting;
using Progress = Code.Runtime.Logic.Progress;

namespace Code.Runtime.Services.Interactions
{
    [UsedImplicitly]
    internal sealed class ReadingTableInteractionService : IReadingTableInteractionService
    {
        private readonly IBookSlotInteractionService _bookSlotInteractionService;
        private readonly IPlayerProgressService _playerProgressService;

        public ReadingTableInteractionService(IBookSlotInteractionService bookSlotInteractionService, IPlayerProgressService playerProgressService)
        {
            _bookSlotInteractionService = bookSlotInteractionService;
            _playerProgressService = playerProgressService;
        }

        public bool CanInteract(IBookStorage bookStorage, Progress progress) =>
            CanRead(bookStorage, progress);

        public void Interact(IBookStorage bookStorage, Progress progress)
        {
            if(bookStorage.HasBook)
            {
                StartReadingIfPossible(bookStorage, progress);
                return;
            }
            
            progress.Reset();
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
            () => _playerProgressService.Progress.PlayerData.ReadBooks.AddReadBook(bookStorage.CurrentBookId);
    }
}