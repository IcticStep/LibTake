using UnityEngine;

namespace Code.Runtime.Services.InputService
{
    public sealed class InputService : IInputService
    {
        private readonly Input _input = new Input();
        
        public Vector2 GetMovement() =>
            _input.MainActionMap.Movement.ReadValue<Vector2>();
    }
}