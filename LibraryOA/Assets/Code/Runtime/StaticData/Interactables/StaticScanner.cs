using UnityEngine;

namespace Code.Runtime.StaticData.Interactables
{
    [CreateAssetMenu(fileName = "ScannerData", menuName = "Static data/Scanner")]
    public class StaticScanner : ScriptableObject
    {
        [field: SerializeField]
        public GameObject Prefab { get; private set; }

        [field: SerializeField]
        [field: Range(0.1f, 10000f)]
        public float SecondsToScan { get; private set; } = 1f;
    }
}