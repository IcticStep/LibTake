using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.Logic.CameraControl
{
    internal sealed class CameraFollow : MonoBehaviour, ICameraFollow
    {
        [SerializeField]
        private Vector3 _offset;
        [SerializeField]
        private Ease _ease;
        [SerializeField]
        private float _defaultMoveDuration;
        [SerializeField]
        private int _animationUpdateValuesFramesInterval;

        private Transform _target;
        private Transform _transform;
        private Tweener _tweener;
        private float _tweenerElapsedTime = 0;
        private UniTaskCompletionSource _animationCompleteSource;

        public Camera Camera { get; private set; }
        public Transform Target => _target;
        public event Action AnimationFinished; 

        private void Awake()
        {
            _transform = transform;
            Camera = GetComponent<Camera>();
        }

        private void Start() =>
            StartCoroutine(UpdateTweenerEndValueCoroutine());

        private void LateUpdate()
        {
            if(_tweener is null)
                GoToTargetImmediately();
        }

        private IEnumerator UpdateTweenerEndValueCoroutine()
        {
            while(true)
            {
                for(int i = 0; i < _animationUpdateValuesFramesInterval; i++)
                    yield return null;

                if(_tweener is null)
                    continue;
                
                _tweenerElapsedTime += _tweener.Elapsed();
                _tweener.ChangeValues(_transform.position, GetPositionByTarget(Target), _defaultMoveDuration - _tweenerElapsedTime);
            }
        }

        private void GoToTargetImmediately()
        {
            if(Target == null)
                return;
            
            _transform.position = GetPositionByTarget(Target);
        }

        public void MoveToNewTarget(Transform target)
        {
            _target = target;
            AnimateTransition();
        }

        public void MoveToNewTarget(Transform target, float duration)
        {
            _target = target;
            AnimateTransition(duration);
        }
        
        public async UniTaskVoid MoveToNewTargetAsync(Transform target, float duration)
        {
            _target = target;
            AnimateTransition(duration);
            await _animationCompleteSource.Task;
        }

        private void AnimateTransition(float? duration = null)
        {
            _animationCompleteSource?.TrySetCanceled();
            _animationCompleteSource = new UniTaskCompletionSource();
            
            KillCurrentTweener();
            _tweener = CreateTransitionTween(duration);
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

        private Tweener CreateTransitionTween(float? duration) =>
            transform
                .DOMove(GetPositionByTarget(Target), duration ?? _defaultMoveDuration)
                .SetEase(_ease)
                .OnComplete(OnTweenerCompete);

        private void OnTweenerCompete()
        {
            KillCurrentTweener();
            AnimationFinished?.Invoke();
            _animationCompleteSource.TrySetResult();
            _animationCompleteSource = null;
        }

        private Vector3 GetPositionByTarget(Transform target) =>
            target.position + _offset;
    }
}