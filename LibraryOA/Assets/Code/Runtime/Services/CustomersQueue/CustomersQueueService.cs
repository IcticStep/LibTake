using System.Collections.Generic;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Customers;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.CustomersQueue
{
    [UsedImplicitly]
    internal sealed class CustomersQueueService : ICustomersQueueService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly Queue<QueueMember> _members = new();
        private IReadOnlyList<Vector3> _points;
        
        public int Capacity => _points.Count;
        public int MembersCount => _members.Count;
        public bool Full => Capacity == MembersCount;

        public void Initialize(IReadOnlyList<Vector3> queuePoints) =>
            _points = queuePoints;

        public void Enqueue(QueueMember queueMember)
        {
            _members.Enqueue(queueMember);
            UpdateMembersPoints();
        }
        
        public void Dequeue()
        {
            QueueMember memberGone = _members.Dequeue();
            memberGone.UpdatePoint(null);
            UpdateMembersPoints();
        }

        private void UpdateMembersPoints()
        {
            int index = 0;
            foreach(QueueMember queueMember in _members)
            {
                queueMember.UpdatePoint(_points[index]);
                if(index == 0)
                    queueMember.InformBecameFirst();
                index++;
            }
        }
    }
}