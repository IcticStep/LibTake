using Code.Runtime.StaticData.Level.MarkersStaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Services.TruckDriving
{
    internal interface ITruckDriveService
    {
        GameObject Truck { get; }
        void RegisterTruck(GameObject truck);
        UniTask DriveToLibrary();
        UniTask DriveAwayLibrary();
        void CleanUp();
    }
}