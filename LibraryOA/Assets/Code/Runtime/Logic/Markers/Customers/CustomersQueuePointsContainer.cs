using Code.Runtime.Services.CustomersQueue;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Markers.Customers
{
    public sealed class CustomersQueuePointsContainer : MonoBehaviour
    {
        [field: SerializeField]
        public QueuePointMarker[] Points { get; private set; }

#if UNITY_EDITOR
        private ICustomersQueueService _customersQueueService;
        
        [Inject]
        private void Construct(ICustomersQueueService customersQueueService) =>
            _customersQueueService = customersQueueService;
        
        [ContextMenu("Dequeue")]
        private void Dequeue() =>
            _customersQueueService?.Dequeue();
#endif
    }
}