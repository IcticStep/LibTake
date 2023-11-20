using UnityEngine;

namespace Code.Runtime.Services.Interactions.Truck.Path
{
    internal interface ITruckDeliveryService
    {
        GameObject Truck { get; }
        void RegisterTruck(GameObject truck); 
        void DriveToLibrary();
        void CleanUp();
    }
}