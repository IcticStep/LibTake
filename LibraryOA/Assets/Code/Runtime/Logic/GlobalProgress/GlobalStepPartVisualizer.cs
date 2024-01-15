using Code.Runtime.StaticData.GlobalGoals;
using UnityEngine;

namespace Code.Runtime.Logic.GlobalProgress
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
        private bool _rootStepObject;

        public GlobalGoal GlobalGoal => _globalGoal;
        public GlobalStep Step => _globalStep;
        public bool RootStepObject => _rootStepObject;

        public void Visualize() =>
            gameObject.SetActive(_targetStateAfterStep);
    }
}