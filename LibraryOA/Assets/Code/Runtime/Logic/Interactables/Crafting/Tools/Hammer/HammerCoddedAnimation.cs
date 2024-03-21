using System;
using Code.Runtime.Logic.Animations;
using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.Logic.Interactables.Crafting.Tools.Hammer
{
    internal sealed class HammerCoddedAnimation : CoddedAnimation
    {
        [SerializeField]
        private float _movingOutDuration;
        [SerializeField]
        private float _movingInDuration;
        [SerializeField]
        private Ease _movingOutEase;
        [SerializeField]
        private Ease _movingInEase;
        [SerializeField]
        private Vector3 _targetLocalRotation;

        private Sequence _sequence;
        private Transform _transform;
        private Vector3 _startLocalRotation;

        public event Action MovingOut;
        public event Action MovingIn;

        public override bool Playing => _sequence.IsPlaying();

        private void Awake()
        {
            _transform = transform;
            _startLocalRotation = _transform.localRotation.eulerAngles;
            _sequence = CreateSequence();
        }

        public override void StartAnimation() =>
            _sequence.Restart();

        private Sequence CreateSequence() =>
            DOTween
                .Sequence()
                .SetAutoKill(false)
                .AppendCallback(NotifyMovingOut)
                .Append(_transform
                    .DOLocalRotate(_targetLocalRotation, _movingOutDuration)
                    .SetEase(_movingOutEase)
                    .SetLink(gameObject)
                    .SetAutoKill(false))
                .AppendCallback(NotifyMovingIn)
                .Append(_transform
                    .DOLocalRotate(_startLocalRotation, _movingInDuration)
                    .SetEase(_movingInEase)
                    .SetLink(gameObject)
                    .SetAutoKill(false))
                .AppendCallback(NotifyFinished)
                .SetLink(gameObject)
                .Pause();

        private void NotifyMovingIn() =>
            MovingIn?.Invoke();

        private void NotifyMovingOut() =>
            MovingOut?.Invoke();
    }
}