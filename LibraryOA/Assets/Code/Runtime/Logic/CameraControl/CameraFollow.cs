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
        
        private Tweener _tweener;

        public Camera Camera { get; private set; }

        private void Awake()
        {
            _transform = transform;
            Camera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            if(_tweener is not null)
            {
                UpdateTweenerEndValue();
                return;
            }
            
            GoToTargetImmediately();
        }

        private void UpdateTweenerEndValue() =>
            _tweener.ChangeEndValue(GetPositionByTarget(_target));

        private void GoToTargetImmediately()
        {
            if(_target == null)
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
            _tweener = CreateTransitionTween();
        }

        private void KillPreviousAnimationIfAny()
        {
            if(_tweener is null)
                return;

            _tweener.Kill();
            _tweener = null;
        }

        private Tweener CreateTransitionTween() =>
            transform
                .DOMove(GetPositionByTarget(_target), _moveDuration)
                .SetEase(_ease)
                .OnComplete(NullTween);

        private Vector3 GetPositionByTarget(Transform target) =>
            target.position + _offset;

        private void NullTween() =>
            _tweener = null;
    }
}