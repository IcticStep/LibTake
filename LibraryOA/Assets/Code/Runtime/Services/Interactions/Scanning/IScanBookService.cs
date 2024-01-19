using System;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.SaveLoad;

namespace Code.Runtime.Services.Interactions.Scanning
{
    internal interface IScanBookService : ISavedProgress
    {
        bool ScanningAllowed { get; }
        event Action BookScanned;
        event Action<bool> ScanningPermissionChanged;
        void AllowScanning();
        void BlockScanning();
        bool IsScanned(string bookId);
        void ScanBook(string bookId);
        bool CanScanBook(string bookId);
    }
}