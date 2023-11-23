using UnityEngine;

namespace Code.Runtime.Logic.Customers
{
    public sealed class CustomersQueueMarker : MonoBehaviour
    {
        [field: SerializeField]
        public QueuePointMarker[] Points { get; private set; }
    }
}