using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Code.Runtime.Logic.Player.Animations
{
    internal sealed class PlayerCutsceneAnimator : MonoBehaviour
    {
        private const string HandleItemLayerName = "HandleItem";
        private const string DoLayerName = "Do";
        private static readonly int _speedParameter = Animator.StringToHash("SpeedPercents");

        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private float _walkingSwitchDuration;
        [SerializeField]
        private Ease _walkingSwitchEase;
        
        [SerializeField]
        private float _scaleDuration;
        [SerializeField]
        private Ease _scaleEase;

        private int _handleItemLayer;
        private int _doLayerIndex;
        private Transform _transform;

        private void OnValidate() =>
            _animator ??= GetComponentInChildren<Animator>();

        private void Awake()
        {
            _handleItemLayer = _animator.GetLayerIndex(HandleItemLayerName);
            _doLayerIndex = _animator.GetLayerIndex(DoLayerName);
            _transform = transform;
        }

        public void StopWalking() =>
            SmoothlyChangeSpeed(0);

        public void StartWalking() =>
            SmoothlyChangeSpeed(1);

        private void SmoothlyChangeSpeed(float endValue) =>
            DOTween
                .Sequence()
                .Append(DOTween.To(
                        () => _animator.GetFloat(_speedParameter),
                        speed => _animator.SetFloat(_speedParameter, speed),
                        endValue,
                        _walkingSwitchDuration)
                    .SetEase(_walkingSwitchEase)
                    .SetLink(gameObject))
                .Append(_transform
                    .DOScale(0, _scaleDuration)
                    .SetEase(_scaleEase)
                    .SetLink(gameObject))
                .SetLink(gameObject);

        private void StartDoAnimation() =>
            _animator.SetLayerWeight(_doLayerIndex, 1);

        private void StopDoAnimation() =>
            _animator.SetLayerWeight(_doLayerIndex, 0);
    }
}