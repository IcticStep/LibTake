using Code.Runtime.Logic.Interactions;
using UnityEngine;

namespace Code.Runtime.Services.Physics
{
    internal interface IPhysicsService
    {
        Interactable RaycastForInteractable(Vector3 rayStart, Vector3 direction, float maxDistance);
        Vector3 Gravity { get; }
    }
}