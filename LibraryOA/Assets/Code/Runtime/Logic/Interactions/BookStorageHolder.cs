using System;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Logic.Interactions.Data;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    public class BookStorageHolder : MonoBehaviour, IBookStorage, IBookStorageHolder, ISavedProgress
    {
        private string _storageId;
        private IPlayerProgressService _progressService;
        public string CurrentBookId { get; private set; }
        public bool HasBook => !string.IsNullOrWhiteSpace(CurrentBookId);

        public event Action Updated;

        public IBookStorage BookStorage => this;

        [Inject]
        private void Construct(IPlayerProgressService progressService) =>
            _progressService = progressService;

        private void Start() =>
            Updated += UpdateProgress;

        private void OnDestroy() =>
            Updated -= UpdateProgress;

        public void Reset()
        {
            _storageId = null;
            if(HasBook)
                RemoveBook();
        }

        public void Initialize(string storageId, string initialBookId)
        {
            InsertBook(initialBookId);
            _storageId = storageId;
        }

        public void InsertBook(string id)
        {
            if(HasBook)
                throw new InvalidOperationException("Can't insert more than one book into storage!");

            CurrentBookId = id;
            Updated?.Invoke();
        }

        public string RemoveBook()
        {
            if(!HasBook)
                throw new InvalidOperationException("Can't remove book from storage when empty!");

            string removed = CurrentBookId;
            CurrentBookId = string.Empty;
            Updated?.Invoke();
            
            return removed;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            BookData savedData = progress.WorldData.BookHoldersState.GetDataForId(_storageId);

            if(savedData is null)
                return;

            if(HasBook)
                RemoveBook();

            if(string.IsNullOrEmpty(savedData.BookId))
                return;

            InsertBook(savedData.BookId);
        }

        private void UpdateProgress() =>
            UpdateProgress(_progressService.Progress);

        public void UpdateProgress(PlayerProgress progress)
        {
            if(_storageId is null)
                return;
            
            progress.WorldData.BookHoldersState.SetDataForId(_storageId, new BookData(this.CurrentBookId));
        }
    }
}