using System;
using UnityEngine;

namespace Code.Runtime.Services.InputService
{
    public interface IInputService
    {
        bool Enabled { get; }
        event Action InteractButtonPressed;
        void Disable();
        void Enable();
        void RegisterMovementProvider(IInputProvider<Vector2> movementProvide);
        void CleanUp();
        Vector2 GetMovement();
    }
}