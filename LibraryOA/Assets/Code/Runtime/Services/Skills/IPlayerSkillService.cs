using System;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.StaticData.Books;

namespace Code.Runtime.Services.Skills
{
    internal interface IPlayerSkillService : ISavedProgress
    {
        event Action Updated;
        void UpdateSkillsBy(string bookId);
        int GetSkillByBookType(BookType bookType);
        void CleanUp();
    }
}