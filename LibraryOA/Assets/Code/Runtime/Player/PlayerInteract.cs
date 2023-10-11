using Code.Runtime.Logic.Interactions;
using Code.Runtime.Services.InputService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Player
{
    internal sealed class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private InteractablesScanner _interactablesScanner;
        private IInputService _input;

        [Inject]
        private void Construct(IInputService input) =>
            _input = input;

        private void Start() =>
            _input.InteractButtonPressed += InteractIfPossible;

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