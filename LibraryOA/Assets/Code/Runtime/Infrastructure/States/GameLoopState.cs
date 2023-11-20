using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Services.TruckDriving;
using Code.Runtime.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class GameLoopState : IState
    {
        private readonly ITruckDriveService _truckDriveService;
        private readonly IStaticDataService _staticDataService;
        private LevelStaticData _levelStaticData;

        public GameLoopState(ITruckDriveService truckDriveService, IStaticDataService staticDataService)
        {
            _truckDriveService = truckDriveService;
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
            await _truckDriveService.DriveToLibrary(_levelStaticData.TruckWay);
        } 
    }
}