using Code.Runtime.Logic.Interactions;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Physics
{
    internal sealed class PhysicsService : IPhysicsService
    {
        private readonly RaycastHit[] _singleHitBuffer = new RaycastHit[1];
        private readonly LayerMask _interactableLayerMask = 1 << LayerMask.NameToLayer("Interactable");

        public Interactable RaycastForInteractable(Vector3 rayStart, Vector3 direction, float maxDistance)
        {
            ClearSingleHitBuffer();
            return UnityEngine.Physics.RaycastNonAlloc(rayStart, direction, _singleHitBuffer, maxDistance, _interactableLayerMask) > 0 
                ? _singleHitBuffer[0].collider.GetComponent<Interactable>() 
                : default(Interactable);
        }

        private RaycastHit ClearSingleHitBuffer() =>
            _singleHitBuffer[0] = default(RaycastHit);
    }
}