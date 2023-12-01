using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    internal interface ICharactersFactory
    {
        GameObject CreatePlayer(Vector3 at);
    }
}