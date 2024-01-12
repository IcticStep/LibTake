using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.StaticData.GlobalGoals
{
    [CreateAssetMenu(fileName = "Global Goal", menuName = "Static data/Global Goals/Global Goal", order = 0)]
    public class GlobalGoal : ScriptableObject
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private List<GlobalStep> _globalSteps;
        
        public string Name => _name;
        public IReadOnlyList<GlobalStep> GlobalSteps => _globalSteps;
    }
}