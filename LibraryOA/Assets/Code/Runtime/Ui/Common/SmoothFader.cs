using System;
using System.Threading;
using Code.Runtime.Infrastructure.Services.CleanUp;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.Common
{
    internal sealed class SmoothFader : MonoBehaviour
    {
        private const float AlphaMinimalDelta = 0.01f;

        [SerializeField]
        private CanvasGroup _canvasGroup;
        [SerializeField]
        private float _fadeDuration;
        [SerializeField]
        private float _unFadeDuration;
        [SerializeField]
        private Ease _fadeEase = Ease.InOutCubic;
        [SerializeField]
        private Ease _unFadeEase = Ease.InOutCubic;
        [SerializeField]
        private bool _ignoreTimeScale;

        private Tween _fadeTween;
        private Tween _unFadeTween;
        private CancellationTokenSource _cancellationTokenSource;
        private ILevelCleanUpService _cleanUpService;

        public bool IsFullyVisible => _canvasGroup.alpha > 1 - AlphaMinimalDelta;
        public bool IsFullyInvisible => _canvasGroup.alpha == 0;
        public bool AnimationInProgress => _fadeTween.IsPlaying() || _unFadeTween.IsPlaying();
        private CancellationToken CancellationToken => _cancellationTokenSource.Token;

        [Inject]
        private void Construct(ILevelCleanUpService cleanUpService) =>
            _cleanUpService = cleanUpService;

        private void Awake()
        {
            if(_fadeTween is null || _unFadeTween is null)
                CreateTweens();

            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());
        }

        private void OnDestroy()
        {
            _fadeTween.Kill();
            _unFadeTween.Kill();
        }

        public void Initialize(float unFadeDuration, float fadeDuration)
        {
            _fadeDuration = fadeDuration;
            _unFadeDuration = unFadeDuration;

            CreateTweens();
        }

        public void Initialize(float unFadeDuration, float fadeDuration, Ease unFadeEase, Ease fadeEase)
        {
            _fadeDuration = fadeDuration;
            _unFadeDuration = unFadeDuration;
            _fadeEase = fadeEase;
            _unFadeEase = unFadeEase;

            CreateTweens();
        }

        public UniTask FadeAsync()
        {
            try
            {
                if(_fadeTween.IsPlaying())
                    return UniTask.WaitWhile(_fadeTween.IsPlaying, cancellationToken: CancellationToken);

                if(_canvasGroup.alpha == 0)
                    return UniTask.CompletedTask;

                _unFadeTween.Pause();
                _fadeTween.Restart();
                return _fadeTween.AwaitForComplete(cancellationToken: CancellationToken);
            }
            catch(OperationCanceledException)
            {
                // ignored
            }

            return UniTask.CompletedTask;
        }

        public void Fade()
        {
            if(_fadeTween.IsPlaying())
                return;

            _unFadeTween.Pause();
            _fadeTween.Restart();
        }

        public void FadeImmediately()
        {
            PauseTweens();
            _canvasGroup.alpha = 0;
        }

        public UniTask UnFadeAsync()
        {
            try
            {
                if(_unFadeTween.IsPlaying())
                    return UniTask.WaitWhile(_fadeTween.IsPlaying, cancellationToken: CancellationToken);

                if(_canvasGroup.alpha == 1)
                    return UniTask.CompletedTask;

                _fadeTween.Pause();
                _unFadeTween.Restart();
                return _unFadeTween.AwaitForComplete(cancellationToken: CancellationToken);
            }
            catch(OperationCanceledException)
            {
                // ignored
            }
            
            return UniTask.CompletedTask;
        }

        public void UnFade()
        {
            if(_unFadeTween.IsPlaying())
                return;

            _fadeTween.Pause();
            _unFadeTween.Restart();
        }

        public void UnFadeImmediately()
        {
            PauseTweens();
            _canvasGroup.alpha = 1;
        }

        private void CreateTweens()
        {
            _fadeTween = DOTween
                .To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, _fadeDuration)
                .SetEase(_fadeEase)
                .SetAutoKill(false)
                .SetUpdate(_ignoreTimeScale)
                .SetLink(gameObject)
                .Pause();

            _unFadeTween = DOTween
                .To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1, _unFadeDuration)
                .SetEase(_unFadeEase)
                .SetAutoKill(false)
                .SetUpdate(_ignoreTimeScale)
                .SetLink(gameObject)
                .Pause();
        }

        private void PauseTweens()
        {
            _fadeTween.Pause();
            _unFadeTween.Pause();
        }
    }
}