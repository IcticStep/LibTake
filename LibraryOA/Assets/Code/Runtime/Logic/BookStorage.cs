using System;
using UnityEngine;

namespace Code.Runtime.Logic
{
    internal sealed class BookStorage : MonoBehaviour
    {
        [field: SerializeField] public bool HasBook { get; private set; }

        public event Action Updated;
        
        public void AddBook()
        {
            HasBook = true;
            Updated?.Invoke();
        }

        public void RemoveBook()
        {
            HasBook = false;
            Updated?.Invoke();
        }
    }
}