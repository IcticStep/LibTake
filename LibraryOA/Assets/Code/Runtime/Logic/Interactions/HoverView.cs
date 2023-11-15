using Code.Runtime.Logic.Player;
using Code.Runtime.Services.Player;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    internal sealed class HoverView : MonoBehaviour
    {
        [SerializeField] private Interactable _interactable;
        [SerializeField] private MeshRenderer _targetMeshRenderer;
        [SerializeField] private Material _hoverMaterial;

        private IPlayerProviderService _playerProviderService;
        private Material _defaultMaterial;
        
        private InteractablesScanner InteractablesScanner => _playerProviderService.InteractablesScanner;

        [Inject]
        private void Construct(IPlayerProviderService playerProviderService) =>
            _playerProviderService = playerProviderService;

        private void Start()
        {
            InteractablesScanner.FocusedInteractable += OnInteractableFocused;
            InteractablesScanner.UnfocusedInteractable += OnInteractableUnfocused;
            _defaultMaterial = _targetMeshRenderer.material;
        }

        private void OnDestroy()
        {
            InteractablesScanner.FocusedInteractable -= OnInteractableFocused;
            InteractablesScanner.UnfocusedInteractable -= OnInteractableUnfocused;
        }

        private void OnInteractableFocused(Interactable interactable)
        {
            if(interactable.Id != _interactable.Id) 
                return;

            _targetMeshRenderer.material = _hoverMaterial;
        }

        private void OnInteractableUnfocused(Interactable interactable)
        {
            if(interactable.Id != _interactable.Id)
                return;

            _targetMeshRenderer.material = _defaultMaterial;
        }
    }
}