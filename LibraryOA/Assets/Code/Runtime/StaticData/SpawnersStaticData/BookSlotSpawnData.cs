using System;
using Code.Runtime.Logic;
using Code.Runtime.Logic.SpawnMarkers;
using Code.Runtime.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.StaticData.SpawnersStaticData
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

        public BookSlotSpawnData(string id, string initialBookId, Vector3 position)
        {
            Id = id;
            InitialBookId = initialBookId;
            Position = position;
        }
        
        public static BookSlotSpawnData NewFrom(BookSlotSpawn readingTableSpawn) =>
            new(readingTableSpawn.GetComponent<UniqueId>().Id,
                readingTableSpawn.GetComponent<BookSlotSpawn>().InitialBookId,
                readingTableSpawn.transform.position);
    }
}