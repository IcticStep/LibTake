using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.InputService
{
    [UsedImplicitly]
    public sealed class InputService : IInputService
    {
        private readonly Input _input = new Input();

        public InputService()
        {
            _input.Enable();
        }

        public Vector2 GetMovement() =>
            _input.Player.Movement.ReadValue<Vector2>();
    }
}