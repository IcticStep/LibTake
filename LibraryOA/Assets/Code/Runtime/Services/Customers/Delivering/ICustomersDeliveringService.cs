using System.Threading;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Services.Customers.Delivering
{
    internal interface ICustomersDeliveringService
    {
        UniTask DeliverCustomers(CancellationToken cancellationToken);
        void CreateCustomers();
    }
}