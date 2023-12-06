using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Runtime.Services.InputService
{
    [UsedImplicitly]
    public sealed class InputService : IInputService
    {
        private const float MovementMinimal = 0.001f;
        private readonly Input _input = new();
        private readonly List<IInputProvider<Vector2>> _movementFallbackProviders = new();

        public event Action InteractButtonPressed;

        public InputService()
        {
            _input.Enable();
            _input.Player.Interact.started += NotifyInteractButtonPressed;
        }

        public void RegisterMovementProvider(IInputProvider<Vector2> movementProvide) =>
            _movementFallbackProviders.Add(movementProvide);

        public void CleanUp()
        {
            _input.Player.Interact.started -= NotifyInteractButtonPressed;
            _movementFallbackProviders.Clear();
        }

        private void NotifyInteractButtonPressed(InputAction.CallbackContext obj) =>
            InteractButtonPressed?.Invoke();

        public Vector2 GetMovement()
        {
            Vector2 input = _input.Player.Movement.ReadValue<Vector2>();
            ValidateFallbackProviders();
            return HasInput(input) 
                ? input 
                : GetMovementFromFallbackProviders();
        }

        private void ValidateFallbackProviders()
        {
            for(int i = _movementFallbackProviders.Count; i > 0; i--)
            {
                if(_movementFallbackProviders == null)
                    _movementFallbackProviders.RemoveAt(i);
            }
        }

        private Vector2 GetMovementFromFallbackProviders()
        {
            foreach(IInputProvider<Vector2> inputProvider in _movementFallbackProviders)
            {
                Vector2 input = inputProvider.Input;
                if(HasInput(input))
                    return input;
            }

            return Vector2.zero;
        }

        private bool HasInput(Vector2 input) =>
            input.sqrMagnitude > MovementMinimal;
    }
}