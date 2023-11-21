using System;
using UnityEngine;

namespace Code.Runtime.StaticData.SpawnersStaticData
{
    [Serializable]
    public sealed class CustomersPointsData
    {
        [field: SerializeField]
        public Vector3 SpawnPoint { get; private set; }
        [field: SerializeField]
        public Vector3 DespawnPoint { get; private set; }

        public CustomersPointsData(Vector3 spawnPoint, Vector3 despawnPoint)
        {
            SpawnPoint = spawnPoint;
            DespawnPoint = despawnPoint;
        }
    }
}