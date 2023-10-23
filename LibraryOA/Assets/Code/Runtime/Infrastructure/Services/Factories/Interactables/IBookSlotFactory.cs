using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories.Interactables
{
    internal interface IBookSlotFactory
    {
        GameObject Create(string bookSlotId, Vector3 at, string initialBookId = null);
    }
}