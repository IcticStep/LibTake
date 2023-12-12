using Code.Runtime.Ui;

namespace Code.Runtime.Services.Loading
{
    internal interface ILoadingCurtainService
    {
        void Register(LoadingCurtain loadingCurtain);
        void Show();
        void Hide();
    }
}