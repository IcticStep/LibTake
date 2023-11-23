using UnityEngine;

namespace Code.Runtime.Logic.Markers.Customers
{
    public sealed class CustomersWayContainer : MonoBehaviour
    {
        [field: SerializeField]
        public CustomerWayPoint[] WayPoints { get; private set; }
    }
}