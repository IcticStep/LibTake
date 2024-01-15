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

        public GlobalGoal GlobalGoal => _globalGoal;
        public GlobalStep Step => _globalStep;

        public void Visualize() =>
            gameObject.SetActive(_targetStateAfterStep);
    }
}