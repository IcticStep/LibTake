using UnityEngine;

namespace Code.Runtime.Logic.Interactions
{
    internal sealed class BookTable : Interactable
    {
        public override bool CanInteract() =>
            true;

        public override void Interact() =>
            Debug.Log($"Interacted with {nameof(BookTable)}.");
    }
}