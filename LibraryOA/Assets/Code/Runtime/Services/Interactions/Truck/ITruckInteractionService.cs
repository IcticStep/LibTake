namespace Code.Runtime.Services.Interactions.Truck
{
    internal interface ITruckInteractionService
    {
        void PutBookInTruck(string book);
        bool CanInteract();
        bool TryInteract();
        void CleanUp();
    }
}