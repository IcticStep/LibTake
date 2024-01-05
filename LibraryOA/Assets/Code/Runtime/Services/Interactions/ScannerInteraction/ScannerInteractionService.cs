using System;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Interactions;
using Code.Runtime.Logic.Interactions.Data;
using Code.Runtime.Services.Interactions.Scanning;
using Code.Runtime.Services.Player.Inventory;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Interactions.ScannerInteraction
{
    [UsedImplicitly]
    internal sealed class ScannerInteractionService : IScannerInteractionService 
    {
        private readonly IPlayerInventoryService _playerInventoryService;
        private readonly IScanBookService _scanBookService;
        
        public ScannerInteractionService(IPlayerInventoryService playerInventoryService, IScanBookService scanBookService)
        {
            _playerInventoryService = playerInventoryService;
            _scanBookService = scanBookService;
        }

        public void CanInteract(IBookStorage bookStorage, IProgress progress) =>
            CanScan(bookStorage, progress);

        public void Interact(BookStorage bookStorage, Progress progress)
        {
            if(!bookStorage.HasBook && !progress.Empty)
            {
                progress.Reset();
                return;
            }

            StartScanningIfPossible(bookStorage, progress);
        }

        public void StartScanningIfPossible(BookStorage bookStorage, Progress progress)
        {
            if(CanScan(bookStorage, progress))
                progress.StartFilling(GetOnProgressFinishCallback(bookStorage));
        }

        public void StopReading(Progress progress) =>
            throw new System.NotImplementedException();
        
        private bool CanScan(IBookStorage bookStorage, IProgress progress) =>
            bookStorage.HasBook
            && !_playerInventoryService.HasBook
            && progress.CanBeStarted 
            && _scanBookService.CanScanBook(bookStorage.CurrentBookId);
        
        private Action GetOnProgressFinishCallback(IBookStorage bookStorage) =>
            () => _scanBookService.ScanBook(bookStorage.CurrentBookId);
    }
}