using Code.Runtime.Logic;
using Code.Runtime.Logic.Trucking;
using UnityEngine;

namespace Code.Runtime.Services.TruckDriving
{
    public interface ITruckProvider
    {
        Logic.Trucking.TruckDriving TruckDriving { get; }
        Truck Truck { get; }
        void RegisterTruck(GameObject truckGameObject);
        void CleanUp();
    }
}