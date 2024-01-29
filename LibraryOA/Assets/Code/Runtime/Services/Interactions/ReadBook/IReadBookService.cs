using System;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.StaticData.Books;

namespace Code.Runtime.Services.Interactions.ReadBook
{
    internal interface IReadBookService : ISavedProgress
    {
        bool ReadingAllowed { get; }
        event Action<StaticBook> BookRead;
        event Action<bool> ReadingPermissionChanged;
        void AllowReading();
        void BlockReading();
        bool IsRead(string bookId);
        void ReadBook(string bookId);
        bool CanReadBook(string bookId);
        void CleanUp();
    }
}