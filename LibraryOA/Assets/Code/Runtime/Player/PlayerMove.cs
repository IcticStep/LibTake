using Code.Runtime.Services.InputService;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Player
{
    [RequireComponent(typeof(CharacterController))]
    internal sealed class PlayerMove : MonoBehaviour
    {
        private const float MinimalInputThreshold = 0.001f;
        
        [FormerlySerializedAs("_speed")] [SerializeField] private float _baseSpeed = 10f;
        [SerializeField] private float _inertiaDecayRate = 2f;
        
        private IInputService _input;
        private Camera _camera;
        private CharacterController _characterController;
        private Vector3 _currentVelocity;

        private void Awake()
        {
            _camera = Camera.main;
            _characterController = GetComponent<CharacterController>();
        }

        [Inject]
        private void Construct(IInputService input) =>
            _input = input;

        private void Update()
        {
            Vector2 input = _input.GetMovement();
            
            Vector3 direction = GetMovementDirection(input);
            _currentVelocity = CalculateVelocity(input, direction);
            ApplyVelocity();
        }

        private Vector3 GetMovementDirection(Vector2 input)
        {
            Vector3 movementDirection = _camera.transform.TransformDirection(input);
            movementDirection.y = 0;
            movementDirection.Normalize();
            return movementDirection;
        }

        private Vector3 CalculateVelocity(Vector2 input, Vector3 direction)
        {
            if (HasInput(input))
                return _baseSpeed * direction;
            
            return Vector3.Lerp(_currentVelocity, Vector3.zero, _inertiaDecayRate * Time.deltaTime);
        }

        private static bool HasInput(Vector2 input) =>
            input.magnitude > MinimalInputThreshold;

        private void ApplyVelocity() =>
            _characterController.Move(_currentVelocity * Time.deltaTime);
    }
}