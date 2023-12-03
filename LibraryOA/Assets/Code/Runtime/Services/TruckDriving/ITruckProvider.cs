using Code.Runtime.Logic;
using UnityEngine;

namespace Code.Runtime.Services.TruckDriving
{
    public interface ITruckProvider
    {
        Logic.TruckDriving TruckDriving { get; }
        Truck Truck { get; }
        void RegisterTruck(GameObject truckGameObject);
        void CleanUp();
    }
}