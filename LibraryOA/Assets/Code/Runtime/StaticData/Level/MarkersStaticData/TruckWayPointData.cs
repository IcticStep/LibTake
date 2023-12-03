using System;
using Code.Runtime.Data;
using UnityEngine;

namespace Code.Runtime.StaticData.Level.MarkersStaticData
{
    [Serializable]
    public sealed class TruckWayPointData
    {
        [ReadOnly]
        public Vector3 Position;
        [ReadOnly]
        public Quaternion Rotation;

        public TruckWayPointData(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public static TruckWayPointData FromTransform(Transform transform) =>
            new(transform.position, transform.rotation);
    }
}