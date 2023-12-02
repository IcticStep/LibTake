using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.BooksReceiving;
using Code.Runtime.Services.Customers.Pooling;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Customers.Delivering
{
    [UsedImplicitly]
    internal sealed class CustomersDeliveringService : ICustomersDeliveringService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerProgressService _progressService;
        private readonly ICustomersPoolingService _customersPool;
        private readonly IBooksReceivingService _booksReceivingService;

        public CustomersDeliveringService(IStaticDataService staticDataService, IPlayerProgressService progressService, ICustomersPoolingService customersPool,
            IBooksReceivingService booksReceivingService)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _customersPool = customersPool;
            _booksReceivingService = booksReceivingService;
        }

        public void CreateCustomers() =>
            _customersPool.CreateCustomers();

        public async UniTask StartDeliveringCustomers()
        {
            int customersToDeliver = _booksReceivingService.BooksInLibrary - _staticDataService.BookReceiving.BooksShouldLeftInLibrary;
        }
    }
}