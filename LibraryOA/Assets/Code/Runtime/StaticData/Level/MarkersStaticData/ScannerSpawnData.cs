using System;
using Code.Runtime.Data;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Markers.Spawns;
using UnityEngine;

namespace Code.Runtime.StaticData.Level.MarkersStaticData
{
    [Serializable]
    public sealed class ScannerSpawnData
    {
        [ReadOnly] 
        public string Id;
        
        [ReadOnly] 
        public string InitialBookId;
        
        [ReadOnly] 
        public Vector3 Position;

        [ReadOnly] 
        public Quaternion Rotation;

        public ScannerSpawnData(string id, string initialBookId, Vector3 position, Quaternion rotation)
        {
            Id = id;
            InitialBookId = initialBookId;
            Position = position;
            Rotation = rotation;
        }

        public static ScannerSpawnData NewFrom(ScannerSpawn scannerSpawn) =>
            new(scannerSpawn.GetComponent<UniqueId>().Id,
                scannerSpawn.GetComponent<ScannerSpawn>().InitialBookId,
                scannerSpawn.transform.position,
                scannerSpawn.transform.rotation);
    }
}