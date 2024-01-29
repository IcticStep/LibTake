using System;
using Code.Runtime.Data.Progress;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Days
{
    [UsedImplicitly]
    internal sealed class DaysService : IDaysService
    {
        public int CurrentDay { get; private set; }

        public event Action Updated;
        
        public void AddDay()
        {
            CurrentDay++;
            Updated?.Invoke();
        }

        public void LoadProgress(GameProgress progress) =>
            CurrentDay = progress.WorldData.TimeData.CurrentDay;

        public void UpdateProgress(GameProgress progress) =>
            progress.WorldData.TimeData.CurrentDay = CurrentDay;

        public void CleanUp() =>
            CurrentDay = 0;
    }
}