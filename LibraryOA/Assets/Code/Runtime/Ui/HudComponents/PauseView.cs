using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.Ui.HudComponents
{
    internal sealed class PauseView : MonoBehaviour
    {
        [SerializeField]
        private FastFlyingFading _fastFlyingFading;
        [SerializeField]
        private SmoothFader _backgroundFader;
        [SerializeField]
        private GraphicRaycaster _windowPauseRaycaster;

        private bool _busy;
        
        private void Start()
        {
            _windowPauseRaycaster.enabled = false;
            _backgroundFader.FadeImmediately();
            _fastFlyingFading.FadeOut(this.GetCancellationTokenOnDestroy());
        }

        public void Pause()
        {
            if(_busy)
                return;
            
            PauseAsync()
                .Forget();
        }

        public void Resume()
        {
            if(_busy)
                return;
            
            ResumeAsync()
                .Forget();
        }

        private async UniTaskVoid PauseAsync()
        {
            _busy = true;
            _windowPauseRaycaster.enabled = false;
            await _backgroundFader.UnFadeAsync();
            await _fastFlyingFading.FadeIn(this.GetCancellationTokenOnDestroy());
            _windowPauseRaycaster.enabled = true;
            _busy = false;
        }

        private async UniTaskVoid ResumeAsync()
        {
            _busy = true;
            _windowPauseRaycaster.enabled = false;
            await _backgroundFader.FadeAsync();
            await _fastFlyingFading.FadeOut(this.GetCancellationTokenOnDestroy());
            _busy = false;
        }
    }
}