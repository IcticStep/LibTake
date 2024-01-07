using Code.Runtime.Logic.Interactables;
using Code.Runtime.Services.InputService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Player
{
    internal sealed class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private InteractablesScanner _interactablesScanner;
        private IInputService _input;

        [Inject]
        private void Construct(IInputService input) =>
            _input = input;

        private void Start() =>
            _input.InteractButtonPressed += InteractIfPossible;
        
        private void OnDestroy() =>
            _input.InteractButtonPressed -= InteractIfPossible;

        public bool CanInteract() =>
            _interactablesScanner.HasFocusedInteractable
            && _interactablesScanner.CurrentFocusedInteractable.CanInteract();

        private void InteractIfPossible()
        {
            if(!_interactablesScanner.HasFocusedInteractable)
                return;

            Interactable interactable = _interactablesScanner.CurrentFocusedInteractable;
            if(!interactable.CanInteract())
                return;
            
            interactable.Interact();
        }
    }
}