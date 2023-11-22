using System;
using UnityEngine;

namespace Code.Runtime.Logic.Customers
{
    public sealed class QueueMember : MonoBehaviour
    {
        public QueuePoint Point { get; private set; }

        public bool ActiveMember => Point is not null;
        public event Action Updated;
        public event Action BecameFirst;
        
        public void UpdatePoint(QueuePoint point)
        {
            Point = point;
            Updated?.Invoke();
        }

        public void InformBecameFirst() =>
            BecameFirst?.Invoke();
    }
}