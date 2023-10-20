using System;
using UnityEngine;

namespace Code.Runtime.StaticData
{
    [Serializable]
    public class BookSlotData
    {
        public readonly string InitialBookId;
        public readonly Vector3 Position;

        public BookSlotData(string initialBookId, Vector3 position)
        {
            InitialBookId = initialBookId;
            Position = position;
        }
    }
}