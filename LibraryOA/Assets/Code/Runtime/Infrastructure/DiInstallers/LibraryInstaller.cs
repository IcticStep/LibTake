using Code.Runtime.Logic.Queue;
using Code.Runtime.Services.CustomersQueue;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure.DiInstallers
{
    internal sealed class LibraryInstaller : MonoInstaller, IInitializable
    {
        [SerializeField]
        private Queue _customersQueue;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LibraryInstaller>().FromInstance(this);
        }

        public void Initialize()
        {
            Container.Resolve<ICustomersQueueService>().Initialize(_customersQueue);
        }
    }
}