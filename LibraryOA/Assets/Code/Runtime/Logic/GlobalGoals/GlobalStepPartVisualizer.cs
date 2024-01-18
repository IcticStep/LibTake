using Code.Runtime.StaticData.GlobalGoals;
using UnityEngine;

namespace Code.Runtime.Logic.GlobalGoals
{
    public sealed class GlobalStepPartVisualizer : MonoBehaviour
    {
        [SerializeField]
        private GlobalGoal _globalGoal;
        
        [SerializeField]
        private GlobalStep _globalStep;
        
        [SerializeField]
        private bool _targetStateAfterStep;
        
        [SerializeField]
        private bool _initialState;
        
        [SerializeField]
        private bool _isCameraTarget;

        [SerializeField]
        private Transform _cameraTarget;

        public GlobalGoal GlobalGoal => _globalGoal;
        public GlobalStep GlobalStep => _globalStep;
        public bool IsCameraTarget => _isCameraTarget;
        public bool TargetStateAfterStep => _targetStateAfterStep;
        public Transform CameraTarget => _cameraTarget;

        public void Visualize() =>
            gameObject.SetActive(TargetStateAfterStep);

        public void Reset() =>
            gameObject.SetActive(_initialState);
    }
}