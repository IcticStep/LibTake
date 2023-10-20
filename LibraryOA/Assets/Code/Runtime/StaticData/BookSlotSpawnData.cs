using System;
using Code.Runtime.Utils;
using UnityEngine;

namespace Code.Runtime.StaticData
{
    [Serializable]
    public class BookSlotSpawnData
    {
        [ReadOnly] public string SlotId;
        [ReadOnly] public string InitialBookId;
        [ReadOnly] public Vector3 Position; 

        public BookSlotSpawnData(string slotId, string initialBookId, Vector3 position)
        {
            SlotId = slotId;
            InitialBookId = initialBookId;
            Position = position;
        }
    }
}