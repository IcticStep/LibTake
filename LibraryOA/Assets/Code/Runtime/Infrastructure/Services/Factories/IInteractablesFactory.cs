using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    internal interface IInteractablesFactory
    {
        GameObject CreateBookSlot(string bookSlotId, Vector3 at, string initialBookId = null);
        GameObject CreateReadingTable(string objectId, Vector3 at, string initialBookId = null);
    }
}