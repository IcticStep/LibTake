using System;
using System.Linq;
using UnityEngine;

namespace Code.Runtime.Logic.GlobalGoals.RocketStart
{
    internal sealed class ParticlesGroup : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem[] _particles;
        
        private Transform[] _transforms;

        private void OnValidate() =>
            _particles ??= GetComponentsInChildren<ParticleSystem>();

        private void Awake() =>
            _transforms = _particles.Select(particle => particle.transform).ToArray();

        public Vector3 GetParticlesScale() =>
            _transforms[0].localScale;

        public void SetParticlesLocalScale(Vector3 value)
        {
            foreach (Transform particleTransform in _transforms)
                particleTransform.localScale = value;
        }
    }
}