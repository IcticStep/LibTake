using System;
using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.StaticData.Ui
{
    [Serializable]
    public class HudData
    {
        [field: SerializeField]
        public UiMessageIntervals MorningMessageIntervals { get; private set; }

        [field: SerializeField]
        public UiMessageIntervals DayMessageIntervals { get; private set; }
        
        [field: SerializeField]
        public float ShowHideAnimationSeconds { get; private set; }
        
        [field: SerializeField]
        public Ease ShowHideAnimationEase { get; private set; }
    }
}