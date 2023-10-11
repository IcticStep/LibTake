using Code.Runtime.Logic.Interactions;
using UnityEngine;

namespace Code.Runtime.Services.Physics
{
    internal interface IPhysicsService
    {
        Vector3 Gravity { get; }
        Interactable RaycastSphereForInteractable(Vector3 position, float radius);
    }
}