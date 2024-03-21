using Code.Runtime.Logic.Animations;
using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.Logic.Interactables.Crafting.Tools.Gear
{
    internal sealed class GearCoddedAnimation : CoddedAnimation
    {
        [SerializeField]
        private float _iterationDuration;
        [SerializeField]
        private float _degreesByIteration;

        private Sequence _sequence;
        private Transform _transform;

        public override bool Playing => _sequence?.IsPlaying() ?? false;

        private void Awake() =>
            _transform = transform;

        public override void StartAnimation() =>
            StartSequence();

        private void StartSequence() =>
            _sequence = DOTween
                .Sequence()
                .SetAutoKill(false)
                .Append(_transform
                    .DOLocalRotate(Vector3.forward * _degreesByIteration, _iterationDuration, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.Linear)
                    .SetLink(gameObject)
                    .SetAutoKill(false))
                .AppendCallback(OnAnimationFinish)
                .SetLink(gameObject);

        private void OnAnimationFinish()
        {
            _sequence = null;
            NotifyFinished();
        }
    }
}