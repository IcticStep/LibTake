using System;
using Code.Runtime.Logic.Animations;
using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.Logic.Interactables.Scanner
{
    internal sealed class ScannerCoddedAnimation : CoddedAnimation
    {
        [SerializeField]
        private GameObject _planeObject;

        [SerializeField]
        private Vector3 _closingRotation;
        [SerializeField]
        private float _closingDuration;
        [SerializeField]
        private float _openingDuration;
        [SerializeField]
        private Ease _closingEase;
        [SerializeField]
        private Ease _openingEase;

        private Vector3 _openingRotation;
        private Sequence _sequence;

        public event Action ScanIteration;

        public override bool Playing => _sequence.IsPlaying();

        private void Awake()
        {
            _openingRotation = _closingRotation * -1;
            _sequence = CreateSequence();
        }

        public override void StartAnimation() =>
            _sequence.Restart();

        private Sequence CreateSequence() =>
            DOTween
                .Sequence()
                .SetAutoKill(false)
                .AppendCallback(NotifyScanIteration)
                .Append(_planeObject.transform
                    .DORotate(_closingRotation, _closingDuration, RotateMode.LocalAxisAdd)
                    .SetEase(_closingEase)
                    .SetAutoKill(false))
                .Append(_planeObject.transform
                    .DORotate(_openingRotation, _openingDuration, RotateMode.LocalAxisAdd)
                    .SetEase(_openingEase)
                    .SetAutoKill(false))
                .AppendCallback(NotifyFinished)
                .Pause();

        private void NotifyScanIteration() =>
            ScanIteration?.Invoke();
    }
}