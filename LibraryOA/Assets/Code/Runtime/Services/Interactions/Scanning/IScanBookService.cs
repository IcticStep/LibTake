using System;
using Code.Runtime.Infrastructure.Services.SaveLoad;

namespace Code.Runtime.Services.Interactions.Scanning
{
    internal interface IScanBookService : ISavedProgress
    {
        bool ScanningAllowed { get; }
        event Action BookScanned;
        void AllowScanning();
        void BlockScanning();
        bool IsScanned(string bookId);
        void ScanBook(string bookId);
        bool CanScanBook(string bookId);
    }
}