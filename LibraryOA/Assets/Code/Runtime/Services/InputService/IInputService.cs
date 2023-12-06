using System;
using UnityEngine;

namespace Code.Runtime.Services.InputService
{
    public interface IInputService
    {
        event Action InteractButtonPressed;
        void CleanUp();
        Vector2 GetMovement();
        void RegisterMovementProvider(IInputProvider<Vector2> movementProvide);
    }
}