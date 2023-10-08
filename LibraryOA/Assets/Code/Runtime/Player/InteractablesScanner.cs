using System;
using Code.Runtime.Infrastructure.Services;
using Code.Runtime.Logic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Player
{
    internal sealed class InteractablesScanner : MonoBehaviour
    {
        [SerializeField] private Transform _rayStartPoint;
        [SerializeField] private float _rayLength;
        
        private IPhysicsService _physicsService;
        private Interactable _focusedInteractable;

        public Interactable FocusedInteractable
        {
            get => _focusedInteractable;
            private set
            {
                _focusedInteractable = value;
                Updated?.Invoke();
                Debug.Log($"Current focused interactable: {nameof(_focusedInteractable)} | {_focusedInteractable}.");
            }
        }

        public event Action Updated;

        [Inject]
        private void Construct(IPhysicsService physicsService) =>
            _physicsService = physicsService;

        private void Update() =>
            FocusedInteractable = RaycastInteractables();

        private Interactable RaycastInteractables()
        {
            Vector3 rayStart = _rayStartPoint.position;
            return _physicsService.RaycastForInteractable(rayStart, Vector3.forward, _rayLength);
        }
    }
}