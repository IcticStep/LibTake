using System;
using Code.Runtime.Infrastructure.Services.SaveLoad;

namespace Code.Runtime.Services.Interactions.ReadBook
{
    internal interface IReadBookService : ISavedProgress
    {
        bool ReadingAllowed { get; }
        void AllowReading();
        void BlockReading();
        void ReadBook(string bookId);
        bool CanReadBook(string bookId);
        bool IsRead(string bookId);
        event Action BookRead;
        event Action SkillGained;
    }
}