using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.Logic.GlobalGoals
{
    public sealed class Rocket : MonoBehaviour
    {
        [SerializeField]
        private GameObject _particlesGroup;
        [SerializeField]
        private GameObject _ladder;
        [SerializeField]
        private float _targetY;
        [SerializeField]
        private float _launchDuration;
        [SerializeField]
        private float _lauhcnDelay;
        [SerializeField]
        private Ease _ease;

        private Transform _transform;

        private void Awake() =>
            _transform = transform;

        public UniTask Launch()
        {
            _particlesGroup.SetActive(true);
            _ladder.SetActive(false);
            return _transform
                .DOMoveY(_targetY, _launchDuration)
                .SetEase(_ease)
                .ToUniTask();
        }
    }
}