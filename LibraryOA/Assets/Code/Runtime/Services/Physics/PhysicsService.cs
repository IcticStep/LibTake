using System.Linq;
using Code.Runtime.Logic.Interactions;
using UnityEngine;

namespace Code.Runtime.Services.Physics
{
    internal sealed class PhysicsService : IPhysicsService
    {
        private readonly Collider[] _hitColliderBuffer10 = new Collider[10];
        private readonly LayerMask _interactableLayerMask = 1 << LayerMask.NameToLayer("Interactable");

        public Vector3 Gravity => UnityEngine.Physics.gravity;
        
        public Interactable RaycastSphereForInteractable(Vector3 position, float radius)
        {
            ClearHitBuffer10();
            int collisions = UnityEngine.Physics.OverlapSphereNonAlloc(position, radius, _hitColliderBuffer10, _interactableLayerMask);
            if(collisions == 0) 
                return default(Interactable);

            Transform selectedTransform = _hitColliderBuffer10
                .Where(value => value is not null)
                .Select(collider => collider.gameObject.GetComponent<Interactable>())
                .Where(interactable => interactable is not null)
                .Select(interactable => interactable.transform)
                .OrderBy(transform => Vector3.Dot(position, transform.position))
                .FirstOrDefault();

            return selectedTransform?.GetComponent<Interactable>();
        }

        private void ClearHitBuffer10()
        {
            for(int i = 0; i < _hitColliderBuffer10.Length; i++)
                _hitColliderBuffer10[i] = default(Collider);
        }
    }
}