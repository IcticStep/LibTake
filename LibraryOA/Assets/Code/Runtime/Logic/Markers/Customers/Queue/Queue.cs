using UnityEngine;

namespace Code.Runtime.Logic.Markers.Customers.Queue
{
    public sealed class Queue : MonoBehaviour
    {
        [field: SerializeField]
        public QueuePoint[] Point { get; private set; }
    }
}