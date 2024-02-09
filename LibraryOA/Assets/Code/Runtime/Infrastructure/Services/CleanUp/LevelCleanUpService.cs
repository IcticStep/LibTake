using Code.Runtime.Infrastructure.Services.Camera;
using Code.Runtime.Infrastructure.Services.UiHud;
using Code.Runtime.Services.Customers.Pooling;
using Code.Runtime.Services.Customers.Queue;
using Code.Runtime.Services.Customers.Registry;
using Code.Runtime.Services.Days;
using Code.Runtime.Services.InputService;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.Services.Interactions.Registry;
using Code.Runtime.Services.Interactions.Scanning;
using Code.Runtime.Services.Interactions.Truck;
using Code.Runtime.Services.Library;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Services.Player.Provider;
using Code.Runtime.Services.Skills;
using Code.Runtime.Services.TruckDriving;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.Services.CleanUp
{
    [UsedImplicitly]
    internal sealed class LevelCleanUpService : ILevelCleanUpService
    {
        private readonly IInputService _inputService;
        private readonly IInteractablesRegistry _interactablesRegistry;
        private readonly IPlayerProviderService _playerProviderService;
        private readonly ICameraProvider _cameraProvider;
        private readonly ITruckProvider _truckProvider;
        private readonly ICustomersQueueService _customersQueueService;
        private readonly IHudProviderService _hudProviderService;
        private readonly ICustomersPoolingService _customersPoolingService;
        private readonly ICustomersRegistryService _customersRegistryService;
        private readonly IPlayerInventoryService _playerInventoryService;
        private readonly IReadBookService _readBookService;
        private readonly ITruckInteractionService _truckInteractionService;
        private readonly IDaysService _daysService;
        private readonly IPlayerSkillService _playerSkillService;
        private readonly IScanBookService _scanBookService;
        private readonly ICraftingService _craftingService;
        private readonly ILibraryService _libraryService;

        public LevelCleanUpService(
            IInputService inputService,
            IInteractablesRegistry interactablesRegistry,
            IPlayerProviderService playerProviderService,
            ICameraProvider cameraProvider,
            ITruckProvider truckProvider,
            ICustomersQueueService customersQueueService,
            IHudProviderService hudProviderService,
            ICustomersPoolingService customersPoolingService,
            ICustomersRegistryService customersRegistryService,
            IPlayerInventoryService playerInventoryService,
            IReadBookService readBookService,
            ITruckInteractionService truckInteractionService,
            IDaysService daysService,
            IPlayerSkillService playerSkillService,
            IScanBookService scanBookService,
            ICraftingService craftingService,
            ILibraryService libraryService)
        {
            _inputService = inputService;
            _interactablesRegistry = interactablesRegistry;
            _playerProviderService = playerProviderService;
            _cameraProvider = cameraProvider;
            _truckProvider = truckProvider;
            _customersQueueService = customersQueueService;
            _hudProviderService = hudProviderService;
            _customersPoolingService = customersPoolingService;
            _customersRegistryService = customersRegistryService;
            _playerInventoryService = playerInventoryService;
            _readBookService = readBookService;
            _truckInteractionService = truckInteractionService;
            _daysService = daysService;
            _playerSkillService = playerSkillService;
            _scanBookService = scanBookService;
            _craftingService = craftingService;
            _libraryService = libraryService;
        }

        public void CleanUp()
        {
            _inputService.CleanUp();
            _interactablesRegistry.CleanUp();
            _playerProviderService.CleanUp();
            _cameraProvider.CleanUp();
            _truckProvider.CleanUp();
            _customersQueueService.CleanUp();
            _hudProviderService.CleanUp();
            _customersPoolingService.CleanUp();
            _customersRegistryService.CleanUp();
            _playerInventoryService.CleanUp();
            _readBookService.CleanUp();
            _truckInteractionService.CleanUp();
            _daysService.CleanUp();
            _playerSkillService.CleanUp();
            _scanBookService.CleanUp();
            _craftingService.CleanUp();
            _libraryService.CleanUp();
        }
    }
}