using Code.Runtime.Services.InputService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Player
{
    [RequireComponent(typeof(CharacterController))]
    internal sealed class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private IInputService _input;
        private Camera _camera;
        private CharacterController _characterController;

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
            
            Vector3 movementVector = _camera.transform.TransformDirection(input);
            movementVector.y = 0;
            movementVector.Normalize();

            _characterController.Move(Time.deltaTime * _speed * movementVector);
        }
    }
}