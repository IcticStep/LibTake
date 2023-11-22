using UnityEngine;
using UnityEngine.AI;

namespace Code.Runtime.Logic.Customers
{
    internal sealed class CustomerQueueNavigator : MonoBehaviour
    {
        [SerializeField]
        private QueueMember _queueMember;
        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        private void OnEnable() =>
            _queueMember.Updated += OnQueuePositionUpdated;

        private void OnDisable() =>
            _queueMember.Updated -= OnQueuePositionUpdated;

        private void OnQueuePositionUpdated()
        {
            if(_queueMember.Point is null)
                return;
            
            _navMeshAgent.destination = _queueMember.Point.Position;
        }
    }
}