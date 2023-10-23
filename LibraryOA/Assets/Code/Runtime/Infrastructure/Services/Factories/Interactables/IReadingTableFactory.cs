using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories.Interactables
{
    internal interface IReadingTableFactory
    {
        GameObject Create(string objectId, Vector3 at, string initialBookId = null);
    }
}