using Code.Runtime.Logic.CameraControl;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Camera
{
    [UsedImplicitly]
    internal sealed class CameraProvider : ICameraProvider
    {
        public CameraFollow CameraFollow { get; private set; }
        public UnityEngine.Camera MainCamera => CameraFollow.Camera;
        private Animator _animator;

        public void Initialize(CameraFollow main)
        {
            CameraFollow = main;
            _animator = CameraFollow.GetComponent<Animator>();
            DisableAnimator();
        }

        public void EnableAnimator() =>
            _animator.enabled = true;

        public void DisableAnimator() =>
            _animator.enabled = false;

        public void DisableFollow() =>
            CameraFollow.enabled = false;

        public void CleanUp()
        {
            CameraFollow = null;
            _animator = null;
        }
    }
}