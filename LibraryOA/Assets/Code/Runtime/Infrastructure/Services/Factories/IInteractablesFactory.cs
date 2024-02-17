using Code.Runtime.Logic.Customers;
using Code.Runtime.StaticData.Level.MarkersStaticData;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    public interface IInteractablesFactory
    {
        GameObject CreateReadingTable(string objectId, Vector3 at, Quaternion rotation, string initialBookId = null);
        GameObject CreateBookSlot(BookSlotSpawnData spawnData);
        GameObject CreateTruck(TruckWayStaticData truckWayData);
        CustomerStateMachine CreateCustomer(Vector3 at);
        GameObject CreateScanner(string objectId, Vector3 at, Quaternion rotation, string initialBookId = null);
        GameObject CreateStatue(string objectId, Vector3 at, Quaternion rotation);
        GameObject CreateCraftingTable(string objectId, Vector3 at, Quaternion rotation);
    }
}