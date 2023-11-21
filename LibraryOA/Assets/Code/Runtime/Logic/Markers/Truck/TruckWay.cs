using UnityEngine;

namespace Code.Runtime.Logic.Markers.Truck
{
    public sealed class TruckWay : MonoBehaviour
    {
        [field: SerializeField]
        public TruckWayPoint LibraryPoint { get; private set; }

        [field: SerializeField]
        public TruckWayPoint HiddenPoint { get; private set; }
    }
}