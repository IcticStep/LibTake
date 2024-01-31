using System.Threading;
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
        private float _duration;
        [SerializeField]
        private Vector3 _awayPosition = Vector3.up * 1000;
        [SerializeField]
        private Vector2 _onScreenPosition;
        [SerializeField]
        private Ease _flyOnScreenEase = Ease.OutElastic;
        [SerializeField]
        private Ease _flyAwayEase = Ease.InOutQuint;

        private void OnValidate() =>
            _rectTransform ??= GetComponent<RectTransform>(); 

        public UniTask FadeIn(CancellationToken cancellationToken)
        {
            _canvasGroup.alpha = 0f;
            _rectTransform.transform.localPosition = _awayPosition;
            _rectTransform
                .DOAnchorPos(_onScreenPosition, _duration) 
                .SetEase(_flyOnScreenEase)
                .ToUniTask(cancellationToken: cancellationToken);

            return _canvasGroup
                .DOFade(1, _duration)
                .ToUniTask(cancellationToken: cancellationToken);
        }
        
        public UniTask FadeOut(CancellationToken cancellationToken)
        {
            _canvasGroup.alpha = 1f;
            _rectTransform.transform.localPosition = Vector3.zero;
            _rectTransform
                .DOAnchorPos(_awayPosition, _duration)
                .SetEase(_flyAwayEase)
                .ToUniTask(cancellationToken: cancellationToken);

            return _canvasGroup
                .DOFade(0, _duration)
                .ToUniTask(cancellationToken: cancellationToken);
        }
    }
}