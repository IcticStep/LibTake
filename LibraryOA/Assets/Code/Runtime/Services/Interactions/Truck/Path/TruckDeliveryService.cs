using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Splines;

namespace Code.Runtime.Services.Interactions.Truck.Path
{
    [UsedImplicitly]
    internal sealed class TruckDeliveryService : ITruckDeliveryService
    {
        public GameObject Truck { get; private set; }

        public void RegisterTruck(GameObject truck) =>
            Truck = truck;

        public void DriveToLibrary()
        {
        }

        public void CleanUp() =>
            Truck = null;
    }
}