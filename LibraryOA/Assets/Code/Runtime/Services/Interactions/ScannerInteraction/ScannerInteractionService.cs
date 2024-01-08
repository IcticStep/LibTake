using System;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Interactables;
using Code.Runtime.Logic.Interactables.Data;
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
        private readonly IStaticDataService _staticDataService;

        public ScannerInteractionService(IPlayerInventoryService playerInventoryService, IScanBookService scanBookService, IStaticDataService staticDataService)
        {
            _playerInventoryService = playerInventoryService;
            _scanBookService = scanBookService;
            _staticDataService = staticDataService;
        }

        public void CanInteract(IBookStorage bookStorage, IProgress progress) =>
            CanScan(bookStorage, progress);

        public void Interact(BookStorage bookStorage, Progress progress, Scanner scanner)
        {
            if(!bookStorage.HasBook && !progress.Empty)
            {
                progress.Reset();
                return;
            }

            StartScanningIfPossible(bookStorage, progress, scanner);
        }

        public void StartScanningIfPossible(BookStorage bookStorage, Progress progress, Scanner scanner)
        {
            if(CanScan(bookStorage, progress))
                progress.StartFilling(GetOnProgressFinishCallback(bookStorage, scanner));
        }

        public void StopReading(Progress progress) =>
            progress.StopFilling();
        
        private bool CanScan(IBookStorage bookStorage, IProgress progress) =>
            bookStorage.HasBook
            && !_playerInventoryService.HasBook
            && progress.CanBeStarted 
            && _scanBookService.CanScanBook(bookStorage.CurrentBookId);
        
        private Action GetOnProgressFinishCallback(IBookStorage bookStorage, Scanner scanner) =>
            () =>
            {
                _scanBookService.ScanBook(bookStorage.CurrentBookId);
                int reward = _staticDataService.Interactables.Scanner.CoinsReward;
                _playerInventoryService.AddCoins(reward);
                scanner.NotifyRewarded(reward);
            };
    }
}