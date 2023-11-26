using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.StaticData.Level
{
    [Serializable]
    public sealed class CustomersData
    {
        [field: SerializeField]
        public Vector3 SpawnPoint { get; private set; }
        
        [SerializeField]
        private List<Vector3> _queuePoints;
        [SerializeField]
        private List<Vector3> _exitWayPoints;
        
        public IReadOnlyList<Vector3> QueuePoints => _queuePoints;

        public IReadOnlyList<Vector3> ExitWayPoints => _exitWayPoints;

        public CustomersData(Vector3 spawnPoint, List<Vector3> queuePoints, List<Vector3> exitWayPoints)
        {
            SpawnPoint = spawnPoint;
            _queuePoints = queuePoints;
            _exitWayPoints = exitWayPoints;
        }
    }
}