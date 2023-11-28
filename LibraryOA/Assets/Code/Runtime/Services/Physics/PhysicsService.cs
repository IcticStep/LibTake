using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Physics
{
    [UsedImplicitly]
    internal sealed class PhysicsService : IPhysicsService
    {
        private readonly LayerMask _interactableLayerMask = 1 << LayerMask.NameToLayer("Interactable");

        public Vector3 Gravity => UnityEngine.Physics.gravity;
        
        public Collider RaycastForInteractable(Vector3 position, float distance, IEnumerable<Vector3> forwardDirections)
        {
            foreach(Vector3 forwardDirection in forwardDirections)
                if(RaycastForInteractable(position, distance, forwardDirection, out RaycastHit hit))
                    return hit.collider;
            return null;
        }

        private bool RaycastForInteractable(Vector3 position, float distance, Vector3 direction, out RaycastHit hit) =>
            UnityEngine.Physics.Raycast(position, direction, out hit, distance, _interactableLayerMask);
    }
}