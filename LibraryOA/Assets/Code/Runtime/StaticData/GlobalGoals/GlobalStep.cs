using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.StaticData.GlobalGoals
{
    [CreateAssetMenu(fileName = "Global Step", menuName = "Static data/Global Goals/Global Step", order = 0)]
    public class GlobalStep : ScriptableObject
    {
        [SerializeField]
        public string Name;
        [SerializeField]
        public float Duration;
        [SerializeField]
        public int Cost;
        [FormerlySerializedAs("LevelRequirements")]
        [SerializeField]
        public List<SkillConstraint> SkillRequirements;
    }
}