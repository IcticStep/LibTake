using UnityEngine;

namespace Code.Runtime.Logic.Queue
{
    public sealed class Queue : MonoBehaviour
    {
        [field: SerializeField]
        public QueuePoint[] Point { get; private set; }
    }
}