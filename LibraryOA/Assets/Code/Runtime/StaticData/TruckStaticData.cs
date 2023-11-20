using UnityEngine;

namespace Code.Runtime.StaticData
{
    [CreateAssetMenu(fileName = "Truck static data", menuName = "Static data/Truck static data")]
    public class TruckStaticData : ScriptableObject
    {
        [field: SerializeField]
        public float DrivingSeconds { get; private set; } = 3f;
    }
}