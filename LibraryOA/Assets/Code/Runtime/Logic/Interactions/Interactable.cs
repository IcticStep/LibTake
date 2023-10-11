using UnityEngine;

namespace Code.Runtime.Logic.Interactions
{
    public abstract class Interactable : MonoBehaviour, IUniqueIdInitializer
    {
        private string _id;
        string IUniqueIdInitializer.Id
        {
            get => _id;
            set => _id = value;
        }
        public string Id => _id;

        public abstract bool CanInteract();
        public abstract void Interact();
    }
}