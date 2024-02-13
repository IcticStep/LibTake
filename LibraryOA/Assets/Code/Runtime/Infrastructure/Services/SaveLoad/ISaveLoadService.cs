using System;
using Code.Runtime.Data.Progress;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    internal interface ISaveLoadService
    {
        bool HasSavedProgress { get; }
        void SaveProgress();
        GameProgress LoadProgress();
        void DeleteProgress();
        event Action Updated;
        event Action Saved;
    }
}