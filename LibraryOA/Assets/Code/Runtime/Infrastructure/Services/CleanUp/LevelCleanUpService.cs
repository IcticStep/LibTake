using Code.Runtime.Infrastructure.Services.Camera;
using Code.Runtime.Infrastructure.Services.UiHud;
using Code.Runtime.Services.Customers.Pooling;
using Code.Runtime.Services.Customers.Queue;
using Code.Runtime.Services.Customers.Registry;
using Code.Runtime.Services.InputService;
using Code.Runtime.Services.Interactions.Registry;
using Code.Runtime.Services.Player.Provider;
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

        public LevelCleanUpService(
            IInputService inputService,
            IInteractablesRegistry interactablesRegistry,
            IPlayerProviderService playerProviderService,
            ICameraProvider cameraProvider,
            ITruckProvider truckProvider,
            ICustomersQueueService customersQueueService,
            IHudProviderService hudProviderService,
            ICustomersPoolingService customersPoolingService,
            ICustomersRegistryService customersRegistryService)
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
        }
    }
}