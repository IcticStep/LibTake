using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Ui
{
    public sealed class LoadingCurtain : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _blackSmoothFader;
        [SerializeField]
        private SmoothFader _imageSmoothFader;
        [SerializeField]
        private Canvas _canvas;

        public bool BlackVisible =>
            _blackSmoothFader.IsFullyVisible;
        
        public bool ImageVisible =>
            _imageSmoothFader.IsFullyVisible;
        
        private void Start() =>
            DontDestroyOnLoad(_canvas);

        public UniTask ShowImageAsync() =>
            UniTask.WhenAll(
                _imageSmoothFader.UnFadeAsync(),
                _blackSmoothFader.UnFadeAsync());

        public UniTask HideImageAsync() =>
            UniTask.WhenAll(
                _imageSmoothFader.FadeAsync(),
                _blackSmoothFader.FadeAsync());

        public UniTask ShowBlackAsync() =>
            _blackSmoothFader.UnFadeAsync();

        public UniTask HideBlackAsync() =>
            _blackSmoothFader.FadeAsync();

        public void HideBlackImmediately() =>
            _blackSmoothFader.FadeImmediately();

        public void ShowBlackImmediately() =>
            _blackSmoothFader.UnFadeImmediately();
    }
}