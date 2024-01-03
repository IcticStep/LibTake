using Code.Runtime.Data;
using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.Ui.Common
{
    internal sealed class MoverY : MonoBehaviour
    {
        [SerializeField]
        private Ease _ease = Ease.OutQuint;
        [SerializeField]
        private float _duration;
        [SerializeField]
        private float _targetY;
        [SerializeField]
        private bool _resetPositionOnFinish;

        private Tween _tween;
        private RectTransform _transform;
        private Vector3 _startValue;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
            _startValue = _transform.localPosition;
            Vector3 endValue = _startValue.WithY(_targetY);
            
            _tween = DOTween
                .To(() => _transform.localPosition, x => _transform.localPosition = x, endValue, _duration)
                .SetEase(_ease)
                .SetAutoKill(false)
                .Pause();

            if(_resetPositionOnFinish)
                _tween.onStepComplete += ResetPosition;
        }

        public void Move() =>
            _tween.Restart();

        private void ResetPosition() =>
            _transform.localPosition = _startValue;
    }
}