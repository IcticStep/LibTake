using Code.Runtime.Logic.Queue;

namespace Code.Runtime.Services.CustomersQueue
{
    internal interface ICustomersQueueService
    {
        void Initialize(Queue queue);
        void CleanUp();
    }
}