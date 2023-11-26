using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.StaticData.Level.MarkersStaticData;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.TruckDriving
{
    [UsedImplicitly]
    internal sealed class TruckDriveService : ITruckDriveService
    {
        private readonly IStaticDataService _staticDataService;
        public GameObject Truck { get; private set; }

        private float DrivingSeconds => _staticDataService.Interactables.Truck.DrivingSeconds;

        public TruckDriveService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void RegisterTruck(GameObject truck) =>
            Truck = truck;

        public UniTask DriveToLibrary(TruckWayStaticData way) =>
            Truck.transform
                .DOMove(way.LibraryPoint.Position, DrivingSeconds)
                .SetEase(Ease.OutCirc)
                .ToUniTask();

        public void CleanUp() =>
            Truck = null;
    }
}