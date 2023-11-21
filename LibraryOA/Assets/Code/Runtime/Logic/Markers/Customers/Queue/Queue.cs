using UnityEngine;

namespace Code.Runtime.Logic.Markers.CustomersQueue
{
    public sealed class Queue : MonoBehaviour
    {
        [field: SerializeField]
        public QueuePoint[] Point { get; private set; }
    }
}