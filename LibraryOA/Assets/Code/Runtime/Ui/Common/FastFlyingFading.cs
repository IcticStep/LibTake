using System.Threading;
using Code.Runtime.Data;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.Ui.Common
{
    [RequireComponent(typeof(RectTransform))]
    internal sealed class FastFlyingFading : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        [SerializeField]
        private RectTransform _rectTransform;
        [SerializeField]
        private float _offscreenDistance = 1000;
        [SerializeField]
        private float _duration;
        [SerializeField]
        private Direction2d _direction;
        [SerializeField]
        private Ease _flyOnScreenEase = Ease.OutElastic;
        [SerializeField]
        private Ease _flyAwayEase = Ease.InOutQuint;

        private Vector3 _awayPosition;
        private Vector2 _onScreenPosition;
        
        public float Duration => _duration;

        private void OnValidate() =>
            _rectTransform ??= GetComponent<RectTransform>();

        private void Awake()
        {
            _onScreenPosition = _rectTransform.anchoredPosition;
            _awayPosition = _onScreenPosition + _direction.ToVector2() * _offscreenDistance;
        }

        public UniTask FadeIn(CancellationToken cancellationToken)
        {
            _canvasGroup.alpha = 0f;
            _rectTransform.anchoredPosition = _awayPosition;
            _rectTransform
                .DOAnchorPos(_onScreenPosition, Duration) 
                .SetEase(_flyOnScreenEase)
                .ToUniTask(cancellationToken: cancellationToken);

            return _canvasGroup
                .DOFade(1, Duration)
                .ToUniTask(cancellationToken: cancellationToken);
        }
        
        public UniTask FadeOut(CancellationToken cancellationToken)
        {
            _canvasGroup.alpha = 1f;
            _rectTransform.anchoredPosition = _onScreenPosition;
            _rectTransform
                .DOAnchorPos(_awayPosition, Duration)
                .SetEase(_flyAwayEase)
                .ToUniTask(cancellationToken: cancellationToken);

            return _canvasGroup
                .DOFade(0, Duration)
                .ToUniTask(cancellationToken: cancellationToken);
        }

        public void SetInOffScreenPosition() =>
            _rectTransform.anchoredPosition = _awayPosition;
    }
}