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
        public int BooksShouldLeftInLibrary { get; private set; } = 2;
        
        [field: SerializeField]
        public Range BooksInLibraryLimit { get; private set; }

        [field: SerializeField]
        public AnimationCurve BooksDeliveringCurve { get; private set; }
        
        [field: SerializeField]
        public int BooksDeliveringDaysScale { get; private set; }
        
        [field: SerializeField]
        public Range CustomersInterval { get; private set; } = new Range(3, 10);

        [field: SerializeField]
        public float RewardSecondsDelay { get; private set; }

        [field: SerializeField]
        public BookRewards BookRewards { get; private set; } = new();
    }
}