using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Services.Player.Lives;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Interactions.Statue
{
    [UsedImplicitly]
    internal sealed class StatueInteractionService : IStatueInteractionService
    {
        private readonly IPlayerLivesService _playerLivesService;
        private readonly IPlayerInventoryService _playerInventoryService;
        private readonly IStaticDataService _staticDataService;

        private int LifePrice => _staticDataService.Player.LifeRestorePrice;

        public StatueInteractionService(IPlayerLivesService playerLivesService, IPlayerInventoryService playerInventoryService, IStaticDataService staticDataService)
        {
            _playerLivesService = playerLivesService;
            _playerInventoryService = playerInventoryService;
            _staticDataService = staticDataService;
        }

        public bool CanInteract() =>
            _playerLivesService.Lives < _playerLivesService.MaxLives
            && _playerInventoryService.Coins >= LifePrice;

        public void Interact()
        {
            if(!CanInteract())
                return;
            
            _playerInventoryService.RemoveCoins(LifePrice);
            _playerLivesService.RestoreLife();
        }
    }
}