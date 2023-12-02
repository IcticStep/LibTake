using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.StaticData;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Customers.Delivering
{
    [UsedImplicitly]
    internal sealed class CustomersDeliveringService : ICustomersDeliveringService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerProgressService _progressService;
        
        private UniTaskCompletionSource _completionSource;

        public UniTask CustomersDeliveringTask => _completionSource.Task;

        public CustomersDeliveringService(IStaticDataService staticDataService, IPlayerProgressService progressService)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
        }

        public void StartDeliveringCustomers()
        {
            _completionSource = new UniTaskCompletionSource();
        }
    }
}