using System;
using Code.Runtime.Services.Pause;
using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
        private IPauseService _pauseService;

        [Inject]
        private void Construct(IPauseService pauseService) =>
            _pauseService = pauseService;

        private void Start()
        {
            _windowPauseRaycaster.enabled = false;
            _backgroundFader.FadeImmediately();
            _fastFlyingFading.FadeOut(this.GetCancellationTokenOnDestroy());
        }

        private void OnDestroy() =>
            _pauseService.Resume();

        public void Pause()
        {
            if(_busy)
                return;
            
            _pauseService.Pause();
            ShowPauseWindowAsync()
                .Forget();
        }

        public void Resume()
        {
            if(_busy)
                return;
            
            _pauseService.Resume();
            HidePauseWindowAsync()
                .Forget();
        }

        private async UniTaskVoid ShowPauseWindowAsync()
        {
            _busy = true;
            _windowPauseRaycaster.enabled = false;
            await _backgroundFader.UnFadeAsync();
            await _fastFlyingFading.FadeIn(this.GetCancellationTokenOnDestroy());
            _windowPauseRaycaster.enabled = true;
            _busy = false;
        }

        private async UniTaskVoid HidePauseWindowAsync()
        {
            _busy = true;
            _windowPauseRaycaster.enabled = false;
            await _backgroundFader.FadeAsync();
            await _fastFlyingFading.FadeOut(this.GetCancellationTokenOnDestroy());
            _busy = false;
        }
    }
}