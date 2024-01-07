namespace Code.Runtime.Services.Interactions.Statue
{
    internal interface IStatueInteractionService
    {
        bool CanInteract();
        void Interact();
    }
}