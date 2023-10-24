using System;
using Code.Runtime.Logic.Interactions;
using Code.Runtime.Services.Interactions;
using Code.Runtime.Services.Physics;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Player
{
    public sealed class InteractablesScanner : MonoBehaviour
    {
        [SerializeField] 
        private Transform _rayStartPoint;
        [SerializeField] 
        private float _rayLength;
        [SerializeField]
        private bool _logging;
        
        private IPhysicsService _physicsService;
        private Interactable _currentFocusedInteractable;
        private IInteractablesRegistry _interactablesRegistry;

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
                
                if(_logging)
                    Debug.Log($"{nameof(_currentFocusedInteractable)} set to {_currentFocusedInteractable?.name}.");
            }
        }

        public bool HasFocusedInteractable => CurrentFocusedInteractable is not null;

        public event Action Updated;
        public event Action<Interactable> FocusedInteractable;
        public event Action<Interactable> UnfocusedInteractable;

        [Inject]
        private void Construct(IPhysicsService physicsService, IInteractablesRegistry interactablesRegistry)
        {
            _physicsService = physicsService;
            _interactablesRegistry = interactablesRegistry;
        }

        private void Update()
        {
            Interactable raycasted = FindInteractables();
            if(raycasted == CurrentFocusedInteractable) return;
            CurrentFocusedInteractable = raycasted;
        }

        private Interactable FindInteractables()
        {
            Collider found = FindCollider();
            if(found is null)
                return default(Interactable);
            return _interactablesRegistry.GetInteractableByCollider(found);
        }

        private Collider FindCollider() =>
            _physicsService.RaycastSphereForInteractable(
                _rayStartPoint.position,
                _rayStartPoint.TransformDirection(Vector3.forward),
                _rayLength);
    }
}