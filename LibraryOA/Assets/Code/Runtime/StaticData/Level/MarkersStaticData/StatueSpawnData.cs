using System;
using Code.Runtime.Data;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Markers.Spawns;
using UnityEngine;

namespace Code.Runtime.StaticData.Level.MarkersStaticData
{
    [Serializable]
    public sealed class StatueSpawnData
    {
        [ReadOnly] 
        public string Id;
        
        [ReadOnly] 
        public Vector3 Position;

        [ReadOnly] 
        public Quaternion Rotation;

        public StatueSpawnData(string id, Vector3 position, Quaternion rotation)
        {
            Id = id;
            Position = position;
            Rotation = rotation;
        }

        public static StatueSpawnData NewFrom(StatueSpawnMarker scannerSpawn) =>
            new(scannerSpawn.GetComponent<UniqueId>().Id,
                scannerSpawn.transform.position,
                scannerSpawn.transform.rotation);
    }
}