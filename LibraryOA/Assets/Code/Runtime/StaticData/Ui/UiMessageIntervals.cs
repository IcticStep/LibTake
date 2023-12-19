using System;
using UnityEngine;

namespace Code.Runtime.StaticData.Ui
{
    [Serializable]
    public class UiMessageIntervals
    {
        [field: SerializeField]
        public float ShowAnimationTime { get; private set; } = 1f;
        [field: SerializeField]
        public float OnScreenTime { get; private set; } = 2.5f;
        [field: SerializeField]
        public float HideAnimationTime { get; private set; } = 1f;
    }
}