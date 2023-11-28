using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Services.Physics
{
    internal interface IPhysicsService
    {
        Vector3 Gravity { get; }
        Collider RaycastForInteractable(Vector3 position, float distance, IEnumerable<Vector3> forwardDirections);
    }
}