using Code.Runtime.Services.Loading;
using Code.Runtime.Ui;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure.DiInstallers
{
    internal sealed class BootstrapSceneInstaller : MonoInstaller, IInitializable
    {
        [SerializeField]
        private LoadingCurtain _loadingCurtain;

        public override void InstallBindings() =>
            Container.
                BindInterfacesTo<BootstrapSceneInstaller>()
                .FromInstance(this);

        public void Initialize() =>
            Container
                .Resolve<ILoadingCurtainService>()
                .Register(_loadingCurtain);
    }
}