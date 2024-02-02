using Code.Runtime.Ui;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Loading
{
    [UsedImplicitly]
    internal sealed class LoadingCurtainService : ILoadingCurtainService
    {
        private LoadingCurtain _loadingCurtain;
        
        public bool BlackVisible =>
            _loadingCurtain.BlackVisible;
        
        public bool ImageVisible =>
            _loadingCurtain.ImageVisible;

        public void Register(LoadingCurtain loadingCurtain) =>
            _loadingCurtain = loadingCurtain;

        public UniTask ShowImageAsync() =>
            _loadingCurtain.ShowImageAsync();

        public UniTask HideImageAsync() =>
            _loadingCurtain.HideImageAsync();
        
        public UniTask ShowBlackAsync() =>
            _loadingCurtain.ShowBlackAsync();
        
        public UniTask HideBlackAsync() =>
            _loadingCurtain.HideBlackAsync();
        
        public void HideBlackImmediately() =>
            _loadingCurtain.HideBlackImmediately();

        public void ShowBlackImmediately() =>
            _loadingCurtain.ShowBlackImmediately();
    }
}