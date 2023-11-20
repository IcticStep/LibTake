using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.StaticData.SpawnersStaticData;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Interactions.Truck.Path
{
    [UsedImplicitly]
    internal sealed class TruckDeliveryService : ITruckDeliveryService
    {
        public GameObject Truck { get; private set; }
        
        public void RegisterTruck(GameObject truck) =>
            Truck = truck;

        public UniTask DriveToLibrary(TruckWayStaticData way) =>
            Truck.transform
                .DOMove(way.LibraryPoint.Position, 1)
                .ToUniTask();

        public void CleanUp() =>
            Truck = null;
    }
}