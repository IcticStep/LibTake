using Code.Runtime.Data;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Code.Runtime.Logic.Interactions
{
    public class BookStorageHolder : MonoBehaviour, IBookStorageHolder, ISavedProgress
    {
        private readonly BookStorage _bookStorage = new BookStorage();
        private string _storageId;

        public IBookStorage BookStorage => _bookStorage;

        public void Initialize(string storageId, string initialBookId)
        {
            _bookStorage.InsertBook(initialBookId);
            _storageId = storageId;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            BookData savedData = progress.WorldData.GetDataForId(_storageId);

            if(_bookStorage.HasBook)
                _bookStorage.RemoveBook();
            
            if(savedData is null)
                return;
            
            _bookStorage.InsertBook(savedData.BookId);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.SetDataForId(_storageId, new BookData(_bookStorage.CurrentBookId));
        }
    }
}