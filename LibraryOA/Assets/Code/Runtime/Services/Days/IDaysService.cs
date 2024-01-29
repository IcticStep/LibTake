using System;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.SaveLoad;

namespace Code.Runtime.Services.Days
{
    internal interface IDaysService : ISavedProgress
    {
        int CurrentDay { get; }
        event Action Updated;
        void AddDay();
        void CleanUp();
    }
}