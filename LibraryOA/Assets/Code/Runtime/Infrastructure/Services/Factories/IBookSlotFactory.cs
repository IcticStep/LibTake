using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    internal interface IBookSlotFactory
    {
        GameObject Create(string bookSlotId, Vector3 at, string initialBookId = null);
    }
}