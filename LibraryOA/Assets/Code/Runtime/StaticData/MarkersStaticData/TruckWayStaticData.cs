using System;
using Code.Runtime.Logic.Markers.Truck;

namespace Code.Runtime.StaticData.MarkersStaticData
{
    [Serializable]
    public sealed class TruckWayStaticData
    {
        public TruckWayPointData LibraryPoint;
        public TruckWayPointData HiddenPoint;

        public TruckWayStaticData(TruckWayPointData libraryPoint, TruckWayPointData hiddenPoint)
        {
            LibraryPoint = libraryPoint;
            HiddenPoint = hiddenPoint;
        }

        public static TruckWayStaticData FromWayPoints(TruckWayPoint library, TruckWayPoint hidden) =>
            new(TruckWayPointData.FromTransform(library.transform),
                TruckWayPointData.FromTransform(hidden.transform));
    }
}