using System;
using UnityEngine;

namespace Code.Runtime.Logic
{
    internal interface IHasBookInitializer
    {
        public void InitHasBook(bool value);
    }

    internal sealed class BookStorage : MonoBehaviour, IHasBookInitializer
    {
        [SerializeField] private bool _hasBook;

        public bool HasBook
        {
            get => _hasBook;
            private set => _hasBook = value;
        }

        public event Action Updated;

        void IHasBookInitializer.InitHasBook(bool value) =>
            _hasBook = value;

        public void InsertBook()
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