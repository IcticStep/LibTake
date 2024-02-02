using System.Threading.Tasks;
using Code.Runtime.Ui;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Services.Loading
{
    internal interface ILoadingCurtainService
    {
        bool BlackVisible { get; }
        bool ImageVisible { get; }
        void Register(LoadingCurtain loadingCurtain);
        UniTask ShowImageAsync();
        UniTask HideImageAsync();
        UniTask ShowBlackAsync();
        UniTask HideBlackAsync();
        void HideBlackImmediately();
        void ShowBlackImmediately();
    }
}