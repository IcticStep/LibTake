using Code.Runtime.StaticData.SpawnersStaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Services.TruckDriving
{
    internal interface ITruckDriveService
    {
        GameObject Truck { get; }
        void RegisterTruck(GameObject truck);
        public UniTask DriveToLibrary(TruckWayStaticData way);
        void CleanUp();
    }
}