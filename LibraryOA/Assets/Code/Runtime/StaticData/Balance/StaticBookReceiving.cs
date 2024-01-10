using Code.Runtime.Data;
using UnityEngine;

namespace Code.Runtime.StaticData.Balance
{
    [CreateAssetMenu(fileName = "StaticBookReceiving", menuName = "Static data/StaticBookReceiving")]
    public class StaticBookReceiving : ScriptableObject
    {
        [field: SerializeField]
        public float TimeToReceiveBook { get; private set; }
        
        [field: SerializeField]
        public RangeFloat CustomersInterval { get; private set; } = new RangeFloat(3, 10);

        [field: SerializeField]
        public float RewardSecondsDelay { get; private set; }

        [field: SerializeField]
        public BookRewards BookRewards { get; private set; } = new();
    }
}