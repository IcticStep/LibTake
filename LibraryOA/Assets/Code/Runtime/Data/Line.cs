using System;
using UnityEngine;

namespace Code.Runtime.Data
{
    [Serializable]
    public struct Line
    {
        public Vector3 Start { get; private set; }
        public Vector3 End { get; private set; }

        public Vector3 Normalized { get; private set; }

        public Line(Vector3 start, Vector3 end)
        {
            Start = start;
            End = end;
            Normalized = (end - start).normalized;
        }
    }
}