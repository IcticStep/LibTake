using System;
using Code.Runtime.Data;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Markers.Spawns;
using UnityEngine;

namespace Code.Runtime.StaticData.Level.MarkersStaticData
{
    [Serializable]
    public class CraftingTableSpawnData
    {
        [ReadOnly] 
        public string Id;
        
        [ReadOnly] 
        public string InitialBookId;
        
        [ReadOnly] 
        public Vector3 Position;

        [ReadOnly] 
        public Quaternion Rotation;

        public CraftingTableSpawnData(string id, string initialBookId, Vector3 position, Quaternion rotation)
        {
            Id = id;
            InitialBookId = initialBookId;
            Position = position;
            Rotation = rotation;
        }

        public static CraftingTableSpawnData NewFrom(CraftingTableSpawn readingTableSpawn) =>
            new(readingTableSpawn.GetComponent<UniqueId>().Id,
                readingTableSpawn.GetComponent<CraftingTableSpawn>().InitialBookId,
                readingTableSpawn.transform.position,
                readingTableSpawn.transform.rotation);
    }
}