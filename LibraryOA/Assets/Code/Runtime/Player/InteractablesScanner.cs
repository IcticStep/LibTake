using System;
using Code.Runtime.Logic.Interactions;
using Code.Runtime.Services.Physics;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Player
{
    public sealed class InteractablesScanner : MonoBehaviour
    {
        [SerializeField] private Transform _rayStartPoint;
        [SerializeField] private float _rayLength;
        
        private IPhysicsService _physicsService;
        private Interactable _currentFocusedInteractable;

        public float RayLength => _rayLength;
        public Vector3? RayStart => _rayStartPoint != null ? 
            _rayStartPoint.position 
            : null;

        public Interactable CurrentFocusedInteractable
        {
            get => _currentFocusedInteractable;
            private set
            {
                Interactable oldValue = _currentFocusedInteractable;
                _currentFocusedInteractable = value;
                Updated?.Invoke();
                
                if(oldValue != null)
                    UnfocusedInteractable?.Invoke(oldValue);
                if(_currentFocusedInteractable != null && _currentFocusedInteractable != oldValue)
                    FocusedInteractable?.Invoke(_currentFocusedInteractable);
            }
        }

        public bool HasFocusedInteractable => CurrentFocusedInteractable is not null;

        public event Action Updated;
        public event Action<Interactable> FocusedInteractable;
        public event Action<Interactable> UnfocusedInteractable;

        [Inject]
        private void Construct(IPhysicsService physicsService) =>
            _physicsService = physicsService;

        private void Update()
        {
            Interactable raycasted = RaycastInteractables();
            if(raycasted == CurrentFocusedInteractable) return;
            CurrentFocusedInteractable = raycasted;
        }

        private Interactable RaycastInteractables() =>
            _physicsService.RaycastSphereForInteractable(_rayStartPoint.position, _rayStartPoint.TransformDirection(Vector3.forward), _rayLength);

        private void DebugFocusedInteractableChange()
        {
            if (_currentFocusedInteractable is not null)
                Debug.Log($"Current focused interactable: {_currentFocusedInteractable.gameObject.name} | {_currentFocusedInteractable.name}.");
        }
    }
}