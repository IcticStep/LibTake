using System;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Markers.Spawns;
using Code.Runtime.Utils;
using UnityEngine;

namespace Code.Runtime.StaticData.MarkersStaticData
{
    [Serializable]
    public class BookSlotSpawnData
    {
        [ReadOnly] 
        public string Id;
        
        [ReadOnly] 
        public string InitialBookId;
        
        [ReadOnly] 
        public Vector3 Position;

        [ReadOnly]
        public Quaternion TransformRotation;

        public BookSlotSpawnData(string id, string initialBookId, Vector3 position, Quaternion transformRotation)
        {
            Id = id;
            InitialBookId = initialBookId;
            Position = position;
            TransformRotation = transformRotation;
        }
        
        public static BookSlotSpawnData NewFrom(BookSlotSpawn readingTableSpawn) =>
            new(readingTableSpawn.GetComponent<UniqueId>().Id,
                readingTableSpawn.GetComponent<BookSlotSpawn>().InitialBookId,
                readingTableSpawn.transform.position,
                readingTableSpawn.transform.rotation);
    }
}