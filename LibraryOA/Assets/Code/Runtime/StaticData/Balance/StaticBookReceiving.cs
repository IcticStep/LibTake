using System;
using UnityEngine;

namespace Code.Runtime.StaticData.Balance
{
    [CreateAssetMenu(fileName = "StaticBookReceiving", menuName = "Static data/StaticBookReceiving")]
    public class StaticBookReceiving : ScriptableObject
    {
        [field: SerializeField]
        public float TimeToReceiveBook { get; private set; }
        [field: SerializeField]
        public int BooksPerDeliveringAmount { get; private set; } = 5;
        [field: SerializeField]
        public int BooksShouldLeftInLibrary { get; private set; } = 2;
        [field: SerializeField]
        public float CustomersInterval { get; private set; } = 2;
    }
}