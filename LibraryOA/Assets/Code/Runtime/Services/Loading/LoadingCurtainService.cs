using Code.Runtime.Ui;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Loading
{
    [UsedImplicitly]
    internal sealed class LoadingCurtainService : ILoadingCurtainService
    {
        private LoadingCurtain _loadingCurtain;

        public void Register(LoadingCurtain loadingCurtain) =>
            _loadingCurtain = loadingCurtain;

        public void Show() =>
            _loadingCurtain.Show();

        public void Hide() =>
            _loadingCurtain.Hide();
    }
}