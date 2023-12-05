using System;
using Code.Runtime.Infrastructure.Services.SaveLoad;

namespace Code.Runtime.Services.Skills
{
    internal interface ISkillService : ISavedProgress
    {
        event Action Updated;
        void UpdateSkillsBy(string bookId);
    }
}