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
        private readonly IPlayerProgressService _playerProgressService;

        private BooksDeliveringData DeliveringData => _playerProgressService.Progress.WorldData.BooksDeliveringData;

        public TruckInteractionService(IPlayerInventoryService playerInventoryService, IPlayerProgressService playerProgressService)
        {
            _playerInventoryService = playerInventoryService;
            _playerProgressService = playerProgressService;
        }

        public bool CanInteract() =>
            DeliveringData.PreparedForDelivering.Any() && !_playerInventoryService.HasBook;

        public void Interact()
        {
            if(!CanInteract())
                return;

            _playerInventoryService.InsertBooks(DeliveringData.PreparedForDelivering);
            DeliveringData.DeliverPrepared();
        }
    }
}