using External_Dependencies.Joystick_Pack.Scripts.Joysticks;
using UnityEngine;

namespace External_Dependencies.Joystick_Pack.Examples
{
    public class JoystickPlayerExample : MonoBehaviour
    {
        public float speed;
        public VariableJoystick variableJoystick;
        public Rigidbody rb;

        public void FixedUpdate()
        {
            Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
            rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}