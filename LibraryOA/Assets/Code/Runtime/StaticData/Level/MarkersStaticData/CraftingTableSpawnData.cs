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
        public Vector3 Position;

        [ReadOnly] 
        public Quaternion Rotation;

        public CraftingTableSpawnData(string id, Vector3 position, Quaternion rotation)
        {
            Id = id;
            Position = position;
            Rotation = rotation;
        }

        public static CraftingTableSpawnData NewFrom(CraftingTableSpawn readingTableSpawn) =>
            new(readingTableSpawn.GetComponent<UniqueId>().Id,
                readingTableSpawn.transform.position,
                readingTableSpawn.transform.rotation);
    }
}