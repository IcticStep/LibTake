using Code.Runtime.StaticData.SpawnersStaticData;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    internal interface IInteractablesFactory
    {
        GameObject CreateReadingTable(string objectId, Vector3 at, string initialBookId = null);
        GameObject CreateBookSlot(BookSlotSpawnData spawnData);
    }
}