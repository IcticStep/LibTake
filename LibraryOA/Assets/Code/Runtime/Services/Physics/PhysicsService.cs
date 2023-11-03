using System.Linq;
using Code.Runtime.Logic.Interactions;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Physics
{
    [UsedImplicitly]
    internal sealed class PhysicsService : IPhysicsService
    {
        private readonly Collider[] _hitColliderBuffer10 = new Collider[10];
        private readonly LayerMask _interactableLayerMask = 1 << LayerMask.NameToLayer("Interactable");

        public Vector3 Gravity => UnityEngine.Physics.gravity;
        
        public Collider RaycastSphereForInteractable(Vector3 position, Vector3 forwardDirection, float radius)
        {
            int collisions = UnityEngine.Physics.OverlapSphereNonAlloc(position, radius, _hitColliderBuffer10, _interactableLayerMask);
            if(collisions == 0) 
                return default(Collider);

            Collider result = _hitColliderBuffer10[0];
            float resultDot = Vector3.Dot(forwardDirection, result.transform.position - position);
            for(int i = 1; i < collisions; i++)
            {
                Collider current = _hitColliderBuffer10[i];
                Vector3 relativePosition = current.transform.position - position;
                float currentDot = Vector3.Dot(forwardDirection, relativePosition.normalized);
                if(!(currentDot > resultDot))
                    continue;

                result = current;
                resultDot = currentDot;
            }
            
            return result;
        }
    }
}