using Code.Runtime.StaticData.GlobalGoals;
using UnityEngine;
using UnityEngine.Serialization;

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

        public GlobalGoal GlobalGoal => _globalGoal;
        public GlobalStep GlobalStep => _globalStep;

        public void Visualize() =>
            gameObject.SetActive(_targetStateAfterStep);

        public void Reset() =>
            gameObject.SetActive(_initialState);
    }
}