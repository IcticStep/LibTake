using UnityEngine;

namespace Code.Runtime.Logic.Interactions
{
    public abstract class Interactable : MonoBehaviour
    {
        private string _id;
        public string Id => _id;

        public void InitId(string id) =>
            _id = id;

        public abstract bool CanInteract();
        public abstract void Interact();
    }
}