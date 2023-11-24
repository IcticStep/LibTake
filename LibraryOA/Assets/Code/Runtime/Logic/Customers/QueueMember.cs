using System;
using UnityEngine;

namespace Code.Runtime.Logic.Customers
{
    public sealed class QueueMember : MonoBehaviour
    {
        public Vector3? CurrentPoint { get; private set; }
        public bool First { get; private set; }
        public bool ActiveMember => CurrentPoint is not null;

        public event Action Updated;
        public event Action BecameFirst;

        private void Reset()
        {
            CurrentPoint = null;
            First = false;
        }

        public void UpdatePoint(Vector3? point)
        {
            CurrentPoint = point;
            Updated?.Invoke();
        }

        public void InformBecameFirst()
        {
            First = true;
            BecameFirst?.Invoke();
        }
    }
}