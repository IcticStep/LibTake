using System;
using Code.Runtime.Logic;
using Code.Runtime.Logic.SpawnMarkers;
using Code.Runtime.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.StaticData.SpawnersStaticData
{
    [Serializable]
    public class ReadingTableSpawnData
    {
        [ReadOnly] 
        public string Id;
        
        [ReadOnly] 
        public string InitialBookId;
        
        [ReadOnly] 
        public Vector3 Position;

        [ReadOnly] 
        public Quaternion Rotation;

        public ReadingTableSpawnData(string id, string initialBookId, Vector3 position, Quaternion rotation)
        {
            Id = id;
            InitialBookId = initialBookId;
            Position = position;
            Rotation = rotation;
        }

        public static ReadingTableSpawnData NewFrom(ReadingTableSpawn readingTableSpawn) =>
            new(readingTableSpawn.GetComponent<UniqueId>().Id,
                readingTableSpawn.GetComponent<ReadingTableSpawn>().InitialBookId,
                readingTableSpawn.transform.position,
                readingTableSpawn.transform.rotation);
    }
}