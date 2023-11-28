using Code.Runtime.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Physics
{
    [UsedImplicitly]
    internal sealed class PhysicsService : IPhysicsService
    {
        private readonly LayerMask _interactableLayerMask = 1 << LayerMask.NameToLayer("Interactable");

        public Vector3 Gravity => UnityEngine.Physics.gravity;
        
        public Collider RaycastForInteractable(Vector3 position, Vector3 forwardDirection, float distance)
        {
            if(UnityEngine.Physics.Raycast(position, forwardDirection, out RaycastHit hit, distance, _interactableLayerMask))
                return hit.collider;
            return null;
        }
    }
}