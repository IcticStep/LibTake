using System.Collections.Generic;
using Code.Runtime.Logic.Customers;
using UnityEngine;

namespace Code.Runtime.Services.Customers.Queue
{
    internal interface ICustomersQueueService
    {
        int Capacity { get; }
        int MembersCount { get; }
        bool Full { get; }
        void Enqueue(QueueMember queueMember);
        void Dequeue();
        void Initialize(IReadOnlyList<Vector3> queuePoints);
        void CleanUp();
    }
}