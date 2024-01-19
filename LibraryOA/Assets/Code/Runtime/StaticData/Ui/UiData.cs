using UnityEngine;

namespace Code.Runtime.StaticData.Ui
{
    [CreateAssetMenu(fileName = "Ui Data", menuName = "Static data/Ui data")]
    public class UiData : ScriptableObject
    {
        [field: SerializeField]
        public UiMessageIntervals MorningMessageIntervals { get; private set; }
        
        [field: SerializeField]
        public UiMessageIntervals DayMessageIntervals { get; private set; }
        
        [field: SerializeField]
        [field: Header("Additional icons")]
        public Sprite CompletedIcon { get; private set; }
    }
}