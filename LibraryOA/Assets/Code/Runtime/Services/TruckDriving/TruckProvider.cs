using Code.Runtime.Logic;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.TruckDriving
{
    [UsedImplicitly]
    public sealed class TruckProvider : ITruckProvider
    {
        public Truck Truck { get; private set; }
        public Logic.TruckDriving TruckDriving { get; private set; }

        public void RegisterTruck(GameObject truckGameObject)
        {
            Truck = truckGameObject.GetComponentInChildren<Truck>();
            TruckDriving = truckGameObject.GetComponentInChildren<Logic.TruckDriving>();
        }
        
        public void CleanUp() =>
            Truck = null;
    }
}