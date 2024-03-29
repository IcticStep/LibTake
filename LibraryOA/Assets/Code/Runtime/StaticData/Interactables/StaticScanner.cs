using UnityEngine;

namespace Code.Runtime.StaticData.Interactables
{
    [CreateAssetMenu(fileName = "Scanner data", menuName = "Static data/Interactables/Scanner")]
    public class StaticScanner : ScriptableObject
    {
        [field: SerializeField]
        public GameObject Prefab { get; private set; }

        [field: SerializeField]
        [field: Range(0.1f, 10000f)]
        public float SecondsToScan { get; private set; } = 1f;
        
        [field: SerializeField]
        [field: Range(1, 1000)]
        public int CoinsReward { get; private set; } = 1;
    }
}