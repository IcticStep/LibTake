using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Services.Player;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Interactions.Truck
{
    [UsedImplicitly]
    internal sealed class TruckInteractionService : ITruckInteractionService
    {
        private readonly IPlayerInventoryService _playerInventoryService;
        private readonly IPersistantProgressService _persistantProgressService;

        private readonly List<string> _booksInTruck = new();
        
        private BooksDeliveringData DeliveringData => _persistantProgressService.Progress.WorldData.BooksDeliveringData;

        public TruckInteractionService(IPlayerInventoryService playerInventoryService, IPersistantProgressService persistantProgressService)
        {
            _playerInventoryService = playerInventoryService;
            _persistantProgressService = persistantProgressService;
        }

        public void PutBookInTruck(string book) =>
            _booksInTruck.Add(book);

        public bool CanInteract() =>
            _booksInTruck.Any() && !_playerInventoryService.HasBook;

        public bool TryInteract()
        {
            if(!CanInteract())
                return false;

            _playerInventoryService.InsertBooks(_booksInTruck);
            DeliveringData.AddDeliveredBooks(_booksInTruck);
            _booksInTruck.Clear();
            return true;
        }
    }
}