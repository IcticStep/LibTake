using System;
using UnityEngine;

namespace Code.Runtime.Services.InputService
{
    public interface IInputService
    {
        event Action InteractButtonPressed;
        void Dispose();
        Vector2 GetMovement();
        void RegisterMovementProvider(IInputProvider<Vector2> movementProvide);
    }
}