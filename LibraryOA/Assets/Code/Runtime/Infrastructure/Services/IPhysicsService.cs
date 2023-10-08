using Code.Runtime.Logic;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services
{
    internal interface IPhysicsService
    {
        IInteractable RaycastForInteractable(Vector3 rayStart, Vector3 direction, float maxDistance);
    }
}