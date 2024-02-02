using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.Logic.GlobalGoals.RocketStart
{
    [SelectionBase]
    public sealed class Rocket : MonoBehaviour
    {
        [SerializeField]
        private ParticlesGroup _particlesGroup;
        [SerializeField]
        private Ladder _ladder;
        [SerializeField]
        private float _targetY;
        [SerializeField]
        private float _launchDuration;
        [FormerlySerializedAs("_lauhcnDelay")]
        [SerializeField]
        private float _launchDelay;
        [SerializeField]
        private Ease _flyingEase;
        [SerializeField]
        private float _scaleDuration;
        [SerializeField]
        private Ease _scaleEase;
        [SerializeField]
        private float _whileScaleFlyY;
        [SerializeField]
        private Ease _whileScaleFlyEase;

        private Transform _transform;
        private Vector3 _startPosition;
        private Vector3 _startScale;

        private void Awake()
        {
            _transform = transform;
            _startPosition = _transform.position;
            _startScale = _transform.localScale;
        }

        public void ResetLaunch()
        {
            _transform.position = _startPosition;
            _transform.localScale = _startScale;
        }

        public UniTask Launch()
        {
            _particlesGroup.gameObject.SetActive(true);
            return DOTween
                .Sequence()
                .AppendCallback(() => _ladder.Rotate())
                .AppendInterval(_launchDelay)
                .Append(StartFly())
                .Append(Scale())
                .Join(ScaleParticles())
                .Join(ScaleWhileFlying())
                .ToUniTask();
        }

        private TweenerCore<Vector3, Vector3, VectorOptions> Scale() =>
            _transform
                .DOScale(0, _scaleDuration)
                .SetEase(_scaleEase);
        
        private TweenerCore<Vector3, Vector3, VectorOptions> ScaleParticles() =>
            DOTween
                .To(_particlesGroup.GetParticlesScale, _particlesGroup.SetParticlesLocalScale, Vector3.zero, _scaleDuration)
                .SetEase(_scaleEase);

        private TweenerCore<Vector3, Vector3, VectorOptions> ScaleWhileFlying() =>
            _transform
                .DOMoveY(_whileScaleFlyY, _launchDuration)
                .SetEase(_whileScaleFlyEase);

        private TweenerCore<Vector3, Vector3, VectorOptions> StartFly() =>
            _transform
                .DOMoveY(_targetY, _launchDuration)
                .SetEase(_flyingEase);
    }
}