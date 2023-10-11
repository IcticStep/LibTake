using Code.Runtime.Player;
using Code.Runtime.Services.Player;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    internal sealed class HoverView : MonoBehaviour
    {
        [SerializeReference] private Interactable _interactable;
        [SerializeReference] private MeshRenderer _targetMeshRenderer;
        [SerializeField] private Material _hoverMaterial;

        private IPlayerProviderService _playerProviderService;
        private InteractablesScanner _interactablesScanner;
        private Material _defaultMaterial;

        [Inject]
        private void Construct(IPlayerProviderService playerProviderService) =>
            _playerProviderService = playerProviderService;

        private void Start()
        {
            _interactablesScanner = _playerProviderService.Player.GetComponent<InteractablesScanner>();
            _interactablesScanner.FocusedInteractable += OnInteractableFocused;
            _interactablesScanner.UnfocusedInteractable += OnInteractableUnfocused;
            _defaultMaterial = _targetMeshRenderer.material;
        }

        private void OnDestroy()
        {
            _interactablesScanner.FocusedInteractable -= OnInteractableFocused;
            _interactablesScanner.UnfocusedInteractable -= OnInteractableUnfocused;
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