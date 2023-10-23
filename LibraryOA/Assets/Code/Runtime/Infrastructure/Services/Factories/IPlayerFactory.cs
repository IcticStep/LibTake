using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    internal interface IPlayerFactory
    {
        GameObject Create(Vector3 at);
    }
}