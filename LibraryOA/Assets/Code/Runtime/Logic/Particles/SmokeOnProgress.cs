using System;
using UnityEngine;

namespace Code.Runtime.Logic.Particles
{
    internal sealed class SmokeOnProgress : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _smoke;
        [SerializeField]
        private Progress _progress;

        private void Awake()
        {
            _progress.Started += ShowSmoke;
            _progress.Stopped += HideSmoke;
        }

        private void Start() =>
            HideSmoke();

        private void OnDestroy()
        {
            _progress.Started -= ShowSmoke;
            _progress.Stopped -= HideSmoke;
        }

        private void ShowSmoke() =>
            _smoke.Play();

        private void HideSmoke() =>
            _smoke.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}