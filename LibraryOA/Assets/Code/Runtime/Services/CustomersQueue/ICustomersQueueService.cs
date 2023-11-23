using Code.Runtime.Logic.Customers;
using Code.Runtime.StaticData.MarkersStaticData;

namespace Code.Runtime.Services.CustomersQueue
{
    internal interface ICustomersQueueService
    {
        int Capacity { get; }
        int MembersCount { get; }
        bool Full { get; }
        void Initialize(QueueData queueData);
        void Enqueue(QueueMember queueMember);
        void Dequeue();
    }
}