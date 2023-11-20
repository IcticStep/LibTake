using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Services.Interactions.Truck.Path;
using Code.Runtime.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class GameLoopState : IState
    {
        private readonly ITruckDeliveryService _truckDeliveryService;
        private readonly IStaticDataService _staticDataService;
        private LevelStaticData _levelStaticData;

        public GameLoopState(ITruckDeliveryService truckDeliveryService, IStaticDataService staticDataService)
        {
            _truckDeliveryService = truckDeliveryService;
            _staticDataService = staticDataService;
        }

        public void Exit()
        {
            
        }

        public void Start()
        {
            _levelStaticData = _staticDataService.ForLevel(SceneManager.GetActiveScene().name);
            DeliverBooks();
        }

        private async void DeliverBooks()
        {
            await _truckDeliveryService.DriveToLibrary(_levelStaticData.TruckWay);
        } 
    }
}