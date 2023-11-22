using Code.Runtime.Logic.Customers;
using Code.Runtime.Services.CustomersQueue;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Infrastructure.DiInstallers
{
    internal sealed class LibraryInstaller : MonoInstaller, IInitializable
    {
        [FormerlySerializedAs("_customersQueueContainer")]
        [SerializeField]
        private CustomersQueue _customersCustomersQueue;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LibraryInstaller>().FromInstance(this);
        }

        public void Initialize()
        {
            Container.Resolve<ICustomersQueueProvider>().Initialize(_customersCustomersQueue);
        }
    }
}