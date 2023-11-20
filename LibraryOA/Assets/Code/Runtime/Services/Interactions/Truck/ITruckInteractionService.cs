namespace Code.Runtime.Services.Interactions.Truck
{
    internal interface ITruckInteractionService
    {
        bool CanInteract();
        void Interact();
    }
}