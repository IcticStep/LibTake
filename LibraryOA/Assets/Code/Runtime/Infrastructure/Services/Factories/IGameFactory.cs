using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    internal interface IGameFactory
    {
        GameObject CreatePlayer(Vector3 at);
        GameObject CreateBookSlot(string bookSlotId, Vector3 at, string initialBookId = null);
    }
}