using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.StaticData.GlobalGoals
{
    [CreateAssetMenu(fileName = "Global Step", menuName = "Static data/Global Goals/Global Step", order = 0)]
    public class GlobalStep : ScriptableObject
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private List<SkillConstraint> _levelRequirements;
        [SerializeField]
        private float _duration;
        [SerializeField]
        private float _cost;

        public string Name => _name;
        public IReadOnlyList<SkillConstraint> LevelRequirements => _levelRequirements;
        public float Duration => _duration;
        public float Cost => _cost;
    }
}