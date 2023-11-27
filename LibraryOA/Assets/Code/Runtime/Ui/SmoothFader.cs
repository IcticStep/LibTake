using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.Ui
{
    internal sealed class SmoothFader : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        [SerializeField]
        private float _duration;
        
        private Tween _fadeTween;
        private Tween _unFadeTween;

        private void Awake()
        {
            _fadeTween = DOTween
                .To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, _duration)
                .SetAutoKill(false)
                .Pause();
            _unFadeTween = DOTween
                .To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1, _duration)
                .SetAutoKill(false)
                .Pause();
        }

        public void Fade()
        {
            if(_fadeTween.IsPlaying() || _canvasGroup.alpha == 0)
                return;
            
            _unFadeTween.Pause();
            _fadeTween.Restart();
        }

        public void UnFade()
        {
            if(_unFadeTween.IsPlaying() || _canvasGroup.alpha != 0)
                return;
            
            _fadeTween.Pause();
            _unFadeTween.Restart();
        }
    }
}