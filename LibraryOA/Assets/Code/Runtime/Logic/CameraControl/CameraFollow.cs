using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.Logic.CameraControl
{
    internal sealed class CameraFollow : MonoBehaviour, ICameraFollow
    {
        [SerializeField]
        private Vector3 _offset;
        [SerializeField]
        private Ease _ease;
        [SerializeField]
        private float _moveDuration;

        private Transform _target;
        private Transform _transform;
        
        private Tween _tween;

        public Camera Camera { get; private set; }

        private void Awake()
        {
            _transform = transform;
            Camera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            if(_tween is not null)
                return;
            
            GoToTargetImmediately();
        }

        private void GoToTargetImmediately()
        {
            if (_target == null)
                return;
            
            _transform.position = GetPositionByTarget(_target);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
            AnimateTransition();
        }

        private void AnimateTransition()
        {
            KillPreviousAnimationIfAny();
            _tween = CreateTransitionTween();
        }

        private void KillPreviousAnimationIfAny()
        {
            if(_tween is null)
                return;

            _tween.Kill();
            _tween = null;
        }

        private Tween CreateTransitionTween() =>
            transform
                .DOMove(GetPositionByTarget(_target), _moveDuration)
                .SetEase(_ease)
                .OnComplete(NullTween);

        private Vector3 GetPositionByTarget(Transform target) =>
            target.position + _offset;

        private void NullTween() =>
            _tween = null;
    }
}