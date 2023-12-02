using Cysharp.Threading.Tasks;

namespace Code.Runtime.Services.Customers.Delivering
{
    internal interface ICustomersDeliveringService
    {
        UniTask StartDeliveringCustomers();
        void CreateCustomers();
    }
}