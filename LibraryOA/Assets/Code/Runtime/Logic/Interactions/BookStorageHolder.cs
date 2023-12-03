using System;
using Code.Runtime.Data;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Logic.Interactions.Data;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    public class BookStorageHolder : MonoBehaviour, IBookStorageHolder, ISavedProgress
    {
        private readonly BookStorage _bookStorage = new BookStorage();
        private string _storageId;
        private IPlayerProgressService _progressService;

        public IBookStorage BookStorage => _bookStorage;

        [Inject]
        private void Construct(IPlayerProgressService progressService) =>
            _progressService = progressService;

        private void Start() =>
            _bookStorage.Updated += UpdateProgress;

        private void OnDestroy() =>
            _bookStorage.Updated -= UpdateProgress;

        public void Initialize(string storageId, string initialBookId)
        {
            _bookStorage.InsertBook(initialBookId);
            _storageId = storageId;
        }

        public void Reset()
        {
            _storageId = null;
            if(_bookStorage.HasBook)
                _bookStorage.RemoveBook();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            BookData savedData = progress.WorldData.BookHoldersState.GetDataForId(_storageId);

            if(savedData is null)
                return;

            if(_bookStorage.HasBook)
                _bookStorage.RemoveBook();

            if(string.IsNullOrEmpty(savedData.BookId))
                return;

            _bookStorage.InsertBook(savedData.BookId);
        }

        private void UpdateProgress() =>
            UpdateProgress(_progressService.Progress);

        public void UpdateProgress(PlayerProgress progress)
        {
            if(_storageId is null)
                return;
            
            progress.WorldData.BookHoldersState.SetDataForId(_storageId, new BookData(_bookStorage.CurrentBookId));
        }
    }
}