using UnityEngine;

namespace Code.Runtime.Logic.Markers.Customers
{
    public sealed class CustomersQueuePointsContainer : MonoBehaviour
    {
        [field: SerializeField]
        public QueuePointMarker[] Points { get; private set; }
    }
}