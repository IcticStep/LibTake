using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.Logic.CameraControl
{
    internal sealed class CameraLooker : MonoBehaviour
    {
        [SerializeField]
        private float _lookAtStartAnimationDuration;
        
        private Transform _cameraTarget;
        private Tweener _tweener;
        
        private bool TransitionInProgress => _tweener != null && _tweener.IsPlaying();
        
        public Transform CameraTarget
        {
            get => _cameraTarget;
            set
            {
                if(value == null)
                {
                    KillTweenIfAny();
                    return;
                }
                
                _cameraTarget = value;
                KillTweenIfAny();
                PlayStartTransition();
            }
        }

        private void LateUpdate()
        {
            if (CameraTarget == null || TransitionInProgress)
                return;
            
            transform.LookAt(CameraTarget, transform.up);
        }

        private void PlayStartTransition() =>
            _tweener = transform
                .DOLookAt(
                    towards: CameraTarget.position,
                    duration: _lookAtStartAnimationDuration,
                    axisConstraint: AxisConstraint.None)
                .OnComplete(KillTweenIfAny);

        private void KillTweenIfAny()
        {
            if(_tweener == null)
                return;
            
            _tweener.Kill();
            _tweener = null;
        }
    }
}