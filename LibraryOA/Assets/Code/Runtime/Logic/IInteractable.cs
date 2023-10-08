namespace Code.Runtime.Logic
{
    public interface IInteractable
    {
        public bool CanInteract();
        public void Interact();
    }
}