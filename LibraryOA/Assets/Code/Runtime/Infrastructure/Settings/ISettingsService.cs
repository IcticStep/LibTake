using System;

namespace Code.Runtime.Infrastructure.Settings
{
    internal interface ISettingsService
    {
        bool MusicEnabled { get; }
        bool SfxEnabled { get; }
        event Action Updated;
        void TurnOnMusic();
        void TurnOffMusic();
        void TurnOnSfx();
        void TurnOffSfx();
    }
}