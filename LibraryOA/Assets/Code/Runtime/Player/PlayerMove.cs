using Code.Runtime.Services.InputService;
using Code.Runtime.Services.Physics;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Player
{
    [RequireComponent(typeof(CharacterController))]
    internal sealed class PlayerMove : MonoBehaviour
    {
        private const float MinimalInputThreshold = 0.001f;
        
        [SerializeField] private float _baseSpeed = 10f;
        [SerializeField] private float _inertiaDecayRate = 2f;
        [SerializeField] private float _rotationSpeed = 1;

        private IInputService _input;
        private Camera _camera;
        private CharacterController _characterController;
        private Vector3 _currentVelocity;
        private IPhysicsService _physicsService;

        private void Awake()
        {
            _camera = Camera.main;
            _characterController = GetComponent<CharacterController>();
        }

        [Inject]
        private void Construct(IInputService input, IPhysicsService physicsService)
        {
            _input = input;
            _physicsService = physicsService;
        }

        private void Update()
        {
            Vector2 input = _input.GetMovement();
            
            Vector3 direction = GetMovementDirection(input);
            _currentVelocity = CalculateVelocity(input, direction);
            _currentVelocity += _physicsService.Gravity;
            ApplyVelocity();

            if (input != Vector2.zero)
            {
                SetRotation(direction);
            }
        }

        private Vector3 GetMovementDirection(Vector2 input)
        {
            Vector3 movementDirection = _camera.transform.TransformDirection(input);
            movementDirection.y = 0;
            return movementDirection;
        }
        
        private void SetRotation(Vector3 direction)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
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