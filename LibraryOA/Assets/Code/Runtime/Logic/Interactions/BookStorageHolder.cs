using Code.Runtime.Data;
using UnityEngine;

namespace Code.Runtime.Logic.Interactions
{
    public class BookStorageHolder : MonoBehaviour, IBookStorageHolder
    {
        private readonly BookStorage _bookStorage = new BookStorage();

        public IBookStorage BookStorage => _bookStorage;

        public void Initialize(string initialBookId) =>
            _bookStorage.InsertBook(initialBookId);
    }
}