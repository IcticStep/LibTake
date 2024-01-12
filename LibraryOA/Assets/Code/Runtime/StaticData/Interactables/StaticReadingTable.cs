using UnityEngine;

namespace Code.Runtime.StaticData.Interactables
{
    [CreateAssetMenu(fileName = "Reading table data", menuName = "Static data/Interactables/Reading table")]
    public class StaticReadingTable : ScriptableObject
    {
        [field: SerializeField]
        public GameObject Prefab { get; private set; }

        [field: SerializeField]
        [field: Range(0.1f, 10000f)]
        public float SecondsToRead { get; private set; } = 1f;
    }
}