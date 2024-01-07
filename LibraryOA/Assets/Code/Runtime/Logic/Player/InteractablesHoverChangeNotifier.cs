using Code.Runtime.Logic.Interactables;
using Code.Runtime.Logic.Interactables.Api;
using Code.Runtime.Services.Interactions;
using UnityEngine;

namespace Code.Runtime.Logic.Player
{
    internal sealed class InteractablesHoverChangeNotifier : MonoBehaviour
    {
        [SerializeField]
        private InteractablesScanner _interactablesScanner;

        private void Start()
        {
            _interactablesScanner.FocusedInteractable += OnInteractableFocused;
            _interactablesScanner.UnfocusedInteractable += OnInteractableUnfocused;
        }

        private void OnDestroy()
        {
            _interactablesScanner.FocusedInteractable -= OnInteractableFocused;
            _interactablesScanner.UnfocusedInteractable -= OnInteractableUnfocused;
        }

        private void OnInteractableFocused(Interactable interactable)
        {
            if(interactable is IHoverStartListener hoverStartListener)
                hoverStartListener.OnHoverStart();
        }

        private void OnInteractableUnfocused(Interactable interactable)
        {
            if(interactable is IHoverEndListener hoverEndListener)
                hoverEndListener.OnHoverEnd();
        }
    }
}