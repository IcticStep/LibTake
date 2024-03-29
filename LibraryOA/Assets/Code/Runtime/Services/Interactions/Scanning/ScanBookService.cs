using System;
using System.Collections.Generic;
using Code.Runtime.Data.Progress;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Interactions.Scanning
{
    [UsedImplicitly]
    internal sealed class ScanBookService : IScanBookService
    {
        private HashSet<string> _booksScanned = new();
        private List<string> _booksScanCacheForSave;
        
        public bool ScanningAllowed { get; private set; } = true;

        public event Action BookScanned;
        public event Action<bool> ScanningPermissionChanged;

        public void AllowScanning()
        {
            ScanningAllowed = true;
            ScanningPermissionChanged?.Invoke(ScanningAllowed);
        }

        public void BlockScanning()
        {
            ScanningAllowed = false;
            ScanningPermissionChanged?.Invoke(ScanningAllowed);
        }

        public bool IsScanned(string bookId) =>
            _booksScanned.Contains(bookId);

        public void ScanBook(string bookId)
        {
            if(!CanScanBook(bookId))
                throw new InvalidOperationException();
            MarkAsScanned(bookId);
        }

        public bool CanScanBook(string bookId) =>
            !IsScanned(bookId) && ScanningAllowed;

        public void LoadProgress(GameProgress progress) =>
            _booksScanned = new HashSet<string>(progress.PlayerData.BooksScanned);

        public void UpdateProgress(GameProgress progress)
        {
            UpdateBooksScannedCacheForSave();
            progress.PlayerData.BooksScanned = _booksScanCacheForSave;
        }

        public void CleanUp()
        {
            _booksScanned.Clear();
            _booksScanCacheForSave.Clear();
        }

        private void MarkAsScanned(string bookId)
        {
            _booksScanned.Add(bookId);
            BookScanned?.Invoke();
        }

        private void UpdateBooksScannedCacheForSave()
        {
            _booksScanCacheForSave ??= new List<string>(_booksScanned.Count);
            _booksScanCacheForSave.Clear();
            _booksScanCacheForSave.AddRange(_booksScanned);
        }
    }
}