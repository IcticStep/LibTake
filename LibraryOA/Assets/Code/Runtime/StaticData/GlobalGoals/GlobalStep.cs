using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;

namespace Code.Runtime.StaticData.GlobalGoals
{
    [CreateAssetMenu(fileName = "Global Step", menuName = "Static data/Global Goals/Global Step", order = 0)]
    public class GlobalStep : ScriptableObject
    {
        [SerializeField]
        public LocalizedString LocalizedName;
        
        [SerializeField]
        public float Duration;
        
        [SerializeField]
        public int Cost;

        [SerializeField]
        public Sprite Icon;
        
        [FormerlySerializedAs("LevelRequirements")]
        [SerializeField]
        public List<SkillConstraint> SkillRequirements;
    }
}