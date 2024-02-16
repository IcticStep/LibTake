using Code.Runtime.Data;
using UnityEngine;

namespace Code.Runtime.StaticData.Balance
{
    [CreateAssetMenu(fileName = "Book receiving data", menuName = "Static data/Book Receiving")]
    public class StaticBookReceiving : ScriptableObject
    {
        
        [field: SerializeField]
        public ReceivingTimeSettings ReceivingTimeSettings { get; private set; } = new();
        
        [field: SerializeField]
        public RangeFloat CustomersInterval { get; private set; } = new RangeFloat(3, 10);

        [field: SerializeField]
        public float RewardSecondsDelay { get; private set; }

        [field: SerializeField]
        public BookRewards BookRewards { get; private set; } = new();
    }
}