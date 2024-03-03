using System;

namespace Code.Runtime.Infrastructure.Settings
{
    internal interface ISettingsService
    {
        bool MusicEnabled { get; }
        bool SfxEnabled { get; }
        void TurnOnMusic();
        void TurnOffMusic();
        void TurnOnSfx();
        void TurnOffSfx();
        event Action MusicToggled;
        event Action SfxToggled;
    }
}