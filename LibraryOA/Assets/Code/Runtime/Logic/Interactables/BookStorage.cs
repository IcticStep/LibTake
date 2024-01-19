using System;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Logic.Interactables.Data;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactables
{
    public class BookStorage : MonoBehaviour, IBookStorage, ISavedProgress
    {
        private string _storageId;
        private IPersistantProgressService _progressService;
        public string CurrentBookId { get; private set; }
        public bool HasBook => !string.IsNullOrWhiteSpace(CurrentBookId);

        public event Action BooksUpdated;

        [Inject]
        private void Construct(IPersistantProgressService progressService) =>
            _progressService = progressService;

        private void Start() =>
            BooksUpdated += UpdateProgress;

        private void OnDestroy() =>
            BooksUpdated -= UpdateProgress;

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
            BooksUpdated?.Invoke();
        }

        public string RemoveBook()
        {
            if(!HasBook)
                throw new InvalidOperationException("Can't remove book from storage when empty!");

            string removed = CurrentBookId;
            CurrentBookId = string.Empty;
            BooksUpdated?.Invoke();
            
            return removed;
        }

        public void LoadProgress(Runtime.Data.Progress.GameProgress progress)
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

        public void UpdateProgress(Runtime.Data.Progress.GameProgress progress)
        {
            if(_storageId is null)
                return;
            
            progress.WorldData.BookHoldersState.SetDataForId(_storageId, new BookData(this.CurrentBookId));
        }
    }
}