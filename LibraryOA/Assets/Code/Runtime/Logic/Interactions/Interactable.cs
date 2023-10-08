using UnityEngine;

namespace Code.Runtime.Logic.Interactions
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract bool CanInteract();
        public abstract void Interact();
    }
}