using UnityEngine;

namespace Code.Runtime.Logic.Interactions
{
    internal sealed class InteractableProxy : Interactable
    {
        [SerializeField] private Interactable _target;
        
        public override bool CanInteract() =>
            _target.CanInteract();

        public override void Interact() =>
            _target.Interact();
    }
}