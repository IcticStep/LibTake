using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    internal interface ICharactersFactory
    {
        GameObject CreatePlayer(Vector3 at);
        GameObject CreateCustomer(Vector3 at);
    }
}