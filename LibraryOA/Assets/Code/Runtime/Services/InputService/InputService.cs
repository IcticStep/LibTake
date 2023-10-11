using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Runtime.Services.InputService
{
    [UsedImplicitly]
    public sealed class InputService : IInputService, IDisposable
    {
        private readonly Input _input = new Input();

        public event Action InteractButtonPressed;

        public InputService()
        {
            _input.Enable();
            _input.Player.Interact.started += NotifyInteractButtonPressed;
        }

        public void Dispose() =>
            _input.Player.Interact.started -= NotifyInteractButtonPressed;

        private void NotifyInteractButtonPressed(InputAction.CallbackContext obj) =>
            InteractButtonPressed?.Invoke();

        public Vector2 GetMovement() =>
            _input.Player.Movement.ReadValue<Vector2>();
    }
}