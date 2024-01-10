using System.Threading;
using System.Threading.Tasks;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Customers;
using Code.Runtime.Logic.Customers.CustomersStates;
using Code.Runtime.Services.Books.Receiving;
using Code.Runtime.Services.Customers.Pooling;
using Code.Runtime.Services.Random;
using Code.Runtime.StaticData.Balance;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Customers.Delivering
{
    [UsedImplicitly]
    internal sealed class CustomersDeliveringService : ICustomersDeliveringService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ICustomersPoolingService _customersPool;
        private readonly IBooksReceivingService _booksReceivingService;
        private readonly IRandomService _randomService;

        private StaticBookReceiving BookReceiving => _staticDataService.BookReceiving;

        public CustomersDeliveringService(IStaticDataService staticDataService, ICustomersPoolingService customersPool,
            IBooksReceivingService booksReceivingService, IRandomService randomService)
        {
            _staticDataService = staticDataService;
            _customersPool = customersPool;
            _booksReceivingService = booksReceivingService;
            _randomService = randomService;
        }

        public void CreateCustomers() =>
            _customersPool.CreateCustomers();

        public async UniTask DeliverCustomers(CancellationToken cancellationToken)
        {
            int booksInLibrary = _booksReceivingService.BooksInLibrary;
            int booksShouldStayAfter = Mathf.CeilToInt(booksInLibrary * _staticDataService.BookDelivering.PercentsShouldLeftInLibrary);
            int customersToDeliver = booksInLibrary - booksShouldStayAfter;
            Debug.Log($"Customers to deliver: {customersToDeliver}.");
            
            for(int i = 0; i < customersToDeliver; i++)
            {
                await UniTask.WaitUntil(_customersPool.CanActivateMore, cancellationToken: cancellationToken);
                await WaitInterval(cancellationToken);
                if(cancellationToken.IsCancellationRequested)
                    return;
                
                DeliverCustomer();
            }

            await UniTask.WaitUntil(() => _customersPool.ActiveCustomers == 0, cancellationToken: cancellationToken);
        }

        private UniTask WaitInterval(CancellationToken cancellationSourceToken)
        {
            float waitTime = _randomService.GetInRange(BookReceiving.CustomersInterval);
            return UniTask.WaitForSeconds(waitTime, cancellationToken: cancellationSourceToken);
        }

        private void DeliverCustomer()
        {
            Vector3 spawnPoint = _staticDataService.CurrentLevelData.Customers.SpawnPoint;
            ICustomerStateMachine customer = _customersPool.GetCustomer(spawnPoint);
            customer.Enter<QueueMemberState>();
        }
    }
}