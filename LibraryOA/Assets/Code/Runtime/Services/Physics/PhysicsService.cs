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
            ClearHitBuffer10();
            int collisions = UnityEngine.Physics.OverlapSphereNonAlloc(position, radius, _hitColliderBuffer10, _interactableLayerMask);
            if(collisions == 0) 
                return default(Collider);

            Collider result = _hitColliderBuffer10[0];
            float resultDot = Vector3.Dot(forwardDirection, result.transform.position);
            for(int i = 1; i < collisions; i++)
            {
                Collider current = _hitColliderBuffer10[i];
                float currentDot = Vector3.Dot(forwardDirection, position - current.transform.position);
                if(!(currentDot > resultDot))
                    continue;

                result = current;
                resultDot = currentDot;
            }
            
            return result;
        }

        private void ClearHitBuffer10()
        {
            for(int i = 0; i < _hitColliderBuffer10.Length; i++)
                _hitColliderBuffer10[i] = default(Collider);
        }
    }
}