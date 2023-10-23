using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories.Interactables
{
    internal interface IReadingTableFactory
    {
        GameObject Create(string bookSlotId, Vector3 at, string initialBookId = null);
    }
}