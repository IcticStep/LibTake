using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    internal interface IGameFactory
    {
        GameObject CreatePlayer(Vector3 at);
        GameObject CreateBookSlot(string id, bool hasBook, Vector3 transformPosition, Transform transform);
    }
}