using Code.Runtime.Logic.Customers;
using Code.Runtime.StaticData.Level.MarkersStaticData;
using UnityEngine;
using UnityEngine.Splines;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    public interface IInteractablesFactory
    {
        GameObject CreateReadingTable(string objectId, Vector3 at, Quaternion rotation, string initialBookId = null);
        GameObject CreateBookSlot(BookSlotSpawnData spawnData);
        GameObject CreateTruck(TruckWayStaticData truckWayData);
        CustomerStateMachine CreateCustomer(Vector3 at);
        GameObject CreateScanner(string objectId, Vector3 at, Quaternion rotation, string initialBookId = null);
    }
}