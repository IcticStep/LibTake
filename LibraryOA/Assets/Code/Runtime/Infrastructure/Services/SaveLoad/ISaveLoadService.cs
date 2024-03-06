using System;
using Code.Runtime.Data.Progress;
using Code.Runtime.Data.Settings;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    internal interface ISaveLoadService
    {
        bool HasSavedProgress { get; }
        event Action Updated;
        event Action Saved;
        void SaveProgress();
        GameProgress LoadProgress();
        void SaveAudioSettings(AudioSettings audioSettings);
        AudioSettings LoadAudioSettings();
        void DeleteProgress();
    }
}