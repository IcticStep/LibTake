using System.Threading.Tasks;
using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.BooksDelivering;
using Code.Runtime.Services.TruckDriving;
using Code.Runtime.StaticData;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using IState = Code.Runtime.Infrastructure.States.Api.IState;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class GameLoopState : IState
    {
        private readonly ITruckDriveService _truckDriveService;
        private readonly IStaticDataService _staticDataService;
        private readonly IBooksDeliveringService _booksDeliveringService;
        private readonly ICharactersFactory _charactersFactory;

        private LevelStaticData _levelStaticData;

        public GameLoopState(ITruckDriveService truckDriveService, IStaticDataService staticDataService, IBooksDeliveringService booksDeliveringService,
            ICharactersFactory charactersFactory)
        {
            _truckDriveService = truckDriveService;
            _staticDataService = staticDataService;
            _booksDeliveringService = booksDeliveringService;
            _charactersFactory = charactersFactory;
        }

        public void Exit()
        {
            
        }

        public void Start()
        {
            _levelStaticData = _staticDataService.ForLevel(SceneManager.GetActiveScene().name);
            DeliverBooks().Forget();
            SpawnCustomers().Forget();
        }

        private async UniTask DeliverBooks()
        {
            await _truckDriveService.DriveToLibrary(_levelStaticData.TruckWay);
            _booksDeliveringService.DeliverBooks();
        }

        private async UniTask SpawnCustomers()
        {
            for(int i = 0; i < 5; i++)
            {
                await UniTask.WaitForSeconds(0.5f);
                _charactersFactory.CreateCustomer(_levelStaticData.Customers.SpawnPoint);
            }
        }
    }
}