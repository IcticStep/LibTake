using UnityEngine;

namespace Code.Runtime.StaticData
{
    [CreateAssetMenu(fileName = "Level starting settings", menuName = "Static data/Level starting settings", order = 0)]
    public class StaticLevelStartingSettings : ScriptableObject
    {
        [field: SerializeField]
        public float LevelStartDelay { get; private set; }
    }
}