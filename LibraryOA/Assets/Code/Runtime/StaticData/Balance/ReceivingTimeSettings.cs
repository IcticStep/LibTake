using System;
using UnityEngine;

namespace Code.Runtime.StaticData.Balance
{
    [Serializable]
    public class ReceivingTimeSettings
    {
        [field: SerializeField]
        public float TimeToReceiveBook { get; private set; }

        [field: SerializeField]
        public int NotAffectedByAdditionalTimeBooksCount { get; private set; }
        
        [field: SerializeField]
        public float AdditionalTimePerBook { get; private set; }
    }
}