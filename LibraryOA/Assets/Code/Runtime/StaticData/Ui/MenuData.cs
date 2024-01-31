using System;
using UnityEngine;

namespace Code.Runtime.StaticData.Ui
{
    [Serializable]
    public class MenuData
    {
        [field: SerializeField]
        public float StartDelaySeconds { get; private set; }
    }
}