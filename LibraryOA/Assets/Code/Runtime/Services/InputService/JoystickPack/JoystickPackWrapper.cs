using External_Dependencies.Joystick_Pack.Scripts.Base;
using UnityEngine;

namespace Code.Runtime.Services.InputService.JoystickPack
{
    internal sealed class JoystickPackWrapper : IInputProvider<Vector2>
    {
        public override bool Equals(object obj) =>
            ReferenceEquals(this, obj) || obj is JoystickPackWrapper other && Equals(other);

        public override int GetHashCode() =>
            (_joystick != null ? _joystick.GetHashCode() : 0);

        private readonly Joystick _joystick;

        public JoystickPackWrapper(Joystick joystick)
        {
            _joystick = joystick;
        }

        public Vector2 Input => _joystick.Direction;

        private bool Equals(JoystickPackWrapper other) =>
            Equals(_joystick, other._joystick);

        public static bool operator ==(JoystickPackWrapper a, JoystickPackWrapper b)
        {
            if(a == null || b == null)
                return false;
            return a._joystick == b._joystick;
        }

        public static bool operator !=(JoystickPackWrapper a, JoystickPackWrapper b) =>
            !(a == b);
    }
}