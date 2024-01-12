using System.Collections;
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
        [SerializeField]
        private int _animationUpdateValuesFramesInterval;

        private Transform _target;
        private Transform _transform;
        private Tweener _tweener;
        private float _tweenerElapsedTime = 0;

        public Camera Camera { get; private set; }

        private void Awake()
        {
            _transform = transform;
            Camera = GetComponent<Camera>();
        }

        private void Start() =>
            StartCoroutine(UpdateTweenerEndValueCourotine());

        private void LateUpdate()
        {
            if(_tweener is null)
                GoToTargetImmediately();
        }

        private IEnumerator UpdateTweenerEndValueCourotine()
        {
            while(true)
            {
                for(int i = 0; i < _animationUpdateValuesFramesInterval; i++)
                    yield return null;

                if(_tweener is null)
                    continue;
                
                _tweenerElapsedTime += _tweener.Elapsed();
                _tweener.ChangeValues(_transform.position, GetPositionByTarget(_target), _moveDuration - _tweenerElapsedTime);
            }
        }

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
            KillCurrentTweener();
            _tweener = CreateTransitionTween();
            _tweenerElapsedTime = 0;
            _tweener.Play();
        }

        private void KillCurrentTweener()
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
                .OnComplete(KillCurrentTweener);

        private Vector3 GetPositionByTarget(Transform target) =>
            target.position + _offset;
    }
}