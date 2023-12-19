using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.Ui
{
    internal sealed class SmoothFader : MonoBehaviour
    {
        private const float AlphaMinimalDelta = 0.01f;
        
        [SerializeField]
        private CanvasGroup _canvasGroup;
        [SerializeField]
        [FormerlySerializedAs("_duration")]
        private float _fadeDuration;
        [SerializeField]
        [FormerlySerializedAs("_duration")]
        private float _unFadeDuration;
        
        private Tween _fadeTween;
        private Tween _unFadeTween;

        public bool IsFullyVisible => _canvasGroup.alpha > 1 - AlphaMinimalDelta;
        public bool IsFullyInvisible => _canvasGroup.alpha == 0;
        public bool AnimationInProgress => _fadeTween.IsPlaying() || _unFadeTween.IsPlaying();
        
        private void Awake()
        {
            if(_fadeTween is null || _unFadeTween is null)
                CreateTweens(_fadeDuration, _unFadeDuration);
        }

        private void OnDestroy()
        {
            _fadeTween.Kill();
            _unFadeTween.Kill();
        }

        public void Configure(float unFadeDuration, float fadeDuration)
        {
            _fadeDuration = fadeDuration;
            _unFadeDuration = unFadeDuration;
            
            CreateTweens(_fadeDuration, _unFadeDuration);
        }

        public UniTask FadeAsync()
        {
            if(_fadeTween.IsPlaying())
                return UniTask.WaitWhile(_fadeTween.IsPlaying);

            if(_canvasGroup.alpha == 0)
                return UniTask.CompletedTask;
            
            _unFadeTween.Pause();
            _fadeTween.Restart();
            return _fadeTween.AwaitForComplete();
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
            if(_unFadeTween.IsPlaying())
                return UniTask.WaitWhile(_fadeTween.IsPlaying);

            if(_canvasGroup.alpha == 1)
                return UniTask.CompletedTask;
            
            _fadeTween.Pause();
            _unFadeTween.Restart();
            return _unFadeTween.AwaitForComplete();
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

        private void CreateTweens(float fadeDuration, float unFadeDuration)
        {
            _fadeTween = DOTween
                .To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, fadeDuration)
                .SetAutoKill(false)
                .Pause();

            _unFadeTween = DOTween
                .To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1, unFadeDuration)
                .SetAutoKill(false)
                .Pause();
        }

        private void PauseTweens()
        {
            _fadeTween.Pause();
            _unFadeTween.Pause();
        }
    }
}