namespace Code.Runtime.Services.Interactions.Truck
{
    internal interface ITruckInteractionService
    {
        bool CanInteract();
        bool TryInteract();
        void PutBookInTruck(string book);
    }
}