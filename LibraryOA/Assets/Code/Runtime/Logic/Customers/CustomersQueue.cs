using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Logic.Customers
{
    public sealed class CustomersQueue : MonoBehaviour
    {
        [field: SerializeField]
        public QueuePoint[] Points { get; private set; }

        private readonly Queue<QueueMember> _members = new();

        public int Capacity => Points.Length;
        public int MembersCount => _members.Count;
        public bool Full => Capacity == MembersCount;

        public void Enqueue(QueueMember queueMember)
        {
            _members.Enqueue(queueMember);
            UpdateMembersPoints();
        }
        
        public void Dequeue()
        {
            QueueMember memberGone = _members.Dequeue();
            memberGone.UpdatePoint(null);
        }

        private void UpdateMembersPoints()
        {
            int index = 0;
            foreach(QueueMember queueMember in _members)
            {
                queueMember.UpdatePoint(Points[index]);
                if(index == 0)
                    queueMember.InformBecameFirst();
                index++;
            }
        }
    }
}