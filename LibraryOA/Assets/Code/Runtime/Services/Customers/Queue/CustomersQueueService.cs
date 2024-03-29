using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Customers;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Customers.Queue
{
    [UsedImplicitly]
    internal sealed class CustomersQueueService : ICustomersQueueService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly Queue<QueueMember> _membersQueue = new();
        
        private IReadOnlyList<Vector3> _points;
        
        public int Capacity => _points.Count;
        public int MembersCount => _membersQueue.Count;
        public bool Full => Capacity == MembersCount;
        public bool Any => _membersQueue.Any();

        public event Action Updated;
        
        public void Initialize(IReadOnlyList<Vector3> queuePoints) =>
            _points = queuePoints;

        public void Enqueue(QueueMember queueMember)
        {
            _membersQueue.Enqueue(queueMember);
            UpdateMemberPoint(queueMember, _membersQueue.Count-1);
            Updated?.Invoke();
        }
        
        public void Dequeue()
        {
            if(!_membersQueue.Any())
                return;
            
            QueueMember memberGone = _membersQueue.Dequeue();
            memberGone.UpdatePoint(null);
            UpdateMembersPoints();
            Updated?.Invoke();
        }

        public QueueMember Peek() =>
            _membersQueue.Peek();

        public void CleanUp() =>
            _membersQueue.Clear();

        private void UpdateMembersPoints()
        {
            int index = 0;
            foreach(QueueMember queueMember in _membersQueue)
            {
                UpdateMemberPoint(queueMember, index);
                index++;
            }
        }

        private void UpdateMemberPoint(QueueMember queueMember, int index)
        {
            queueMember.UpdatePoint(_points[index]);
            if(index == 0)
                queueMember.InformBecameFirst();
        }
    }
}