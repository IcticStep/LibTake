using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Services.BooksDelivering;
using Code.Runtime.Services.TruckDriving;
using Code.Runtime.StaticData.Level;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class MorningState : IState
    {
        private readonly ITruckDriveService _truckDriveService;
        private readonly IStaticDataService _staticDataService;
        private readonly IBooksDeliveringService _booksDeliveringService;
        private readonly IInteractablesFactory _interactablesFactory;
        private readonly ISaveLoadService _saveLoadService;

        private LevelStaticData _levelStaticData;

        public MorningState(ITruckDriveService truckDriveService, IStaticDataService staticDataService, IBooksDeliveringService booksDeliveringService,
            IInteractablesFactory interactablesFactory, ISaveLoadService saveLoadService)
        {
            _truckDriveService = truckDriveService;
            _staticDataService = staticDataService;
            _booksDeliveringService = booksDeliveringService;
            _interactablesFactory = interactablesFactory;
            _saveLoadService = saveLoadService;
        }

        public void Start()
        {
            _saveLoadService.SaveProgress();
            
            _levelStaticData = _staticDataService.ForLevel(SceneManager.GetActiveScene().name);
            DeliverBooks().Forget();
            SpawnCustomers().Forget();
        }

        public void Exit()
        {
            
        }

        private async UniTask DeliverBooks()
        {
            _booksDeliveringService.DeliverBooks();
            await _truckDriveService.DriveToLibrary();
        }

        private async UniTask SpawnCustomers()
        {
            for(int i = 0; i < 10; i++)
            {
                await UniTask.WaitForSeconds(0.5f);
                _interactablesFactory.CreateCustomer(_levelStaticData.Customers.SpawnPoint);
            }
        }
    }
}