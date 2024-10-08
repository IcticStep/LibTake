using System;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.HudComponents
{
    internal sealed class SaveView : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        [SerializeField]
        private float _onScreenDelay = 0.3f;

        private ISaveLoadService _saveLoadService;
        private bool _forceStop;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService) =>
            _saveLoadService = saveLoadService;

        private void Awake() =>
            _saveLoadService.Saved += OnSaved;

        private void Start() =>
            _smoothFader.FadeImmediately();

        private void OnDestroy()
        {
            _saveLoadService.Saved -= OnSaved;
            _forceStop = true;
        }

        private void OnSaved() =>
            PlayAnimation().Forget();

        private async UniTaskVoid PlayAnimation()
        {
            try
            {
                await _smoothFader.UnFadeAsync();
                if(_forceStop)
                    return;

                await UniTask.WaitForSeconds(_onScreenDelay, cancellationToken: this.GetCancellationTokenOnDestroy());
                if(_forceStop)
                    return;

                await _smoothFader.FadeAsync();
            }
            catch (OperationCanceledException)
            {
                // ignored
            }
        }
    }
}