using System;
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
        
        [field: SerializeField]
        public Transform CameraTargetOnFly { get; private set; }

        private Transform _transform;
        private Vector3 _startPosition;
        private Vector3 _startScale;

        public event Action RocketLaunch;

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

        public void Launch() =>
            LaunchAsync()
                .Forget();

        public UniTask LaunchAsync()
        {
#if UNITY_EDITOR
            // if editor is playing
            if(!Application.isPlaying)
                return UniTask.CompletedTask;
#endif
            RocketLaunch?.Invoke();
            
            _particlesGroup.gameObject.SetActive(true);
            return DOTween
                .Sequence()
                .AppendCallback(() => _ladder.Throw())
                .AppendInterval(_launchDelay)
                .Append(StartFly())
                .Append(Scale())
                .Join(ScaleParticles())
                .Join(ScaleWhileFlying())
                .SetLink(gameObject)
                .ToUniTask(cancellationToken: this.GetCancellationTokenOnDestroy());
        }

        private TweenerCore<Vector3, Vector3, VectorOptions> Scale() =>
            _transform
                .DOScale(0, _scaleDuration)
                .SetLink(gameObject)
                .SetEase(_scaleEase);
        
        private TweenerCore<Vector3, Vector3, VectorOptions> ScaleParticles() =>
            DOTween
                .To(_particlesGroup.GetParticlesScale, _particlesGroup.SetParticlesLocalScale, Vector3.zero, _scaleDuration)
                .SetLink(gameObject)
                .SetEase(_scaleEase);

        private TweenerCore<Vector3, Vector3, VectorOptions> ScaleWhileFlying() =>
            _transform
                .DOMoveY(_whileScaleFlyY, _launchDuration)
                .SetLink(gameObject)
                .SetEase(_whileScaleFlyEase);

        private TweenerCore<Vector3, Vector3, VectorOptions> StartFly() =>
            _transform
                .DOMoveY(_targetY, _launchDuration)
                .SetLink(gameObject)
                .SetEase(_flyingEase);
    }
}