using UnityEngine;

namespace Code.Runtime.StaticData.Interactables
{
    [CreateAssetMenu(fileName = "Truck data", menuName = "Static data/Interactables/Truck")]
    public class StaticTruck : ScriptableObject
    {
        [field: SerializeField]
        public GameObject Prefab { get; private set; }

        [field: SerializeField]
        public float DrivingSeconds { get; private set; } = 3f;
    }
}