using UnityEngine;

namespace Code.Runtime.Logic
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract bool CanInteract();
        public abstract void Interact();
    }
}