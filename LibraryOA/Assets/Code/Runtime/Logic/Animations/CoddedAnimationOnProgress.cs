using UnityEngine;

namespace Code.Runtime.Logic.Animations
{
    internal sealed class CoddedAnimationOnProgress : MonoBehaviour
    {
        [SerializeField]
        private Progress _progress;
        [SerializeField]
        private CoddedAnimation _coddedAnimation;

        private void Awake()
        {
            _progress.Started += OnProgressStart;
            _coddedAnimation.Finished += OnAnimationFinished;
        }

        private void OnDestroy()
        {
            _progress.Started -= OnProgressStart;
            _coddedAnimation.Finished -= OnAnimationFinished;
        }

        private void OnProgressStart()
        {
            if(_coddedAnimation.Playing)
                return;
            
            _coddedAnimation.StartAnimation();
        }

        private void OnAnimationFinished()
        {
            if(!_progress.Running)
                return;
            
            _coddedAnimation.StartAnimation();
        }
    }
}