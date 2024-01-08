using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Data;
using Code.Runtime.Logic.Interactables;
using Code.Runtime.Services.Interactions.Registry;
using Code.Runtime.Services.Physics;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Player
{
    public sealed class InteractablesScanner : MonoBehaviour
    {
        [SerializeField] 
        private Transform _rayStartPoint;
        [SerializeField]
        [Range(0f, 50f)]
        private float _rayLength;
        [SerializeField]
        [Range(0, 50)]
        private int _raysCount = 3;
        [SerializeField]
        [Range(0f, 50f)]
        private float _raysDegreesInterval = 30f;
        [SerializeField]
        private bool _logging;
        
        private IPhysicsService _physicsService;
        private Interactable _currentFocusedInteractable;
        private IInteractablesRegistry _interactablesRegistry;
        private VectorRotator _vectorRotator;
        
        public IReadOnlyList<Line> CurrentRays { get; private set; }
        public Vector3 StartPointForward => _rayStartPoint.transform.forward;
        public float RayLength => _rayLength;
        public Vector3? RayStart => _rayStartPoint != null 
            ? _rayStartPoint.position 
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
                    Debug.Log($"{nameof(_currentFocusedInteractable)} set to {_currentFocusedInteractable!?.name}.");
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
        
        private void Awake() =>
            _vectorRotator = CreateRayVectorsRotator();
        
        private void OnValidate() =>
            _vectorRotator = CreateRayVectorsRotator();

        private void Update()
        {
            CurrentRays = _vectorRotator.CreateVectorsRotated(_rayStartPoint.position, _rayStartPoint.forward);
            Interactable raycasted = FindInteractables(CurrentRays);
            if(raycasted == CurrentFocusedInteractable) 
                return;
            
            CurrentFocusedInteractable = raycasted;
        }

        public VectorRotator CreateRayVectorsRotator() =>
            new(_rayLength, _raysCount, _raysDegreesInterval, Vector3.up);

        private Interactable FindInteractables(IReadOnlyList<Line> rays)
        {
            Collider found = FindCollider(rays);
            return found is null 
                ? null 
                : _interactablesRegistry.GetInteractableByCollider(found);
        }
        
        private Collider FindCollider(IReadOnlyList<Line> rays) =>
            _physicsService.RaycastForInteractable(
                _rayStartPoint.position,
                _rayLength, 
                rays.Select(ray => ray.Normalized));
    }
}