using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.StaticData.MarkersStaticData
{
    [Serializable]
    public sealed class QueueData
    {
        [SerializeField]
        private List<Vector3> _points;

        public IReadOnlyList<Vector3> Points => _points;

        public QueueData(List<Vector3> points) 
        {
            _points = points;
        }
    }
}