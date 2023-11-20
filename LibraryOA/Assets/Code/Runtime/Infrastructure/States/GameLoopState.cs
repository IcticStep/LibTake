using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Services.Interactions.Truck.Path;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class GameLoopState : IState
    {
        private readonly ITruckDeliveryService _truckDeliveryService;

        public GameLoopState(ITruckDeliveryService truckDeliveryService)
        {
            _truckDeliveryService = truckDeliveryService;
        }

        public void Exit()
        {
            
        }

        public void Start()
        {
            _truckDeliveryService.DriveToLibrary();
        }
    }
}