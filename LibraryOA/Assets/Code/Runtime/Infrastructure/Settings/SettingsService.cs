using System;
using Code.Runtime.Data.Settings;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using JetBrains.Annotations;
using Zenject;

namespace Code.Runtime.Infrastructure.Settings
{
    [UsedImplicitly]
    internal sealed class SettingsService : IInitializable, ISettingsService
    {
        private readonly ISaveLoadService _saveLoadService;

        public bool MusicEnabled => _audioSettings.MusicEnabled;
        public bool SfxEnabled => _audioSettings.SfxEnabled;
        
        private AudioSettings _audioSettings;

        public event Action MusicToggled;
        public event Action SfxToggled;
        
        public SettingsService(ISaveLoadService saveLoadService) 
        {
            _saveLoadService = saveLoadService;
        }

        public void Initialize() =>
            _audioSettings = _saveLoadService.LoadAudioSettings();
        
        public void TurnOnMusic()
        {
            _audioSettings.MusicEnabled = true;
            MusicToggled?.Invoke();
            _saveLoadService.SaveAudioSettings(_audioSettings);
        }

        public void TurnOffMusic()
        {
            _audioSettings.MusicEnabled = false;
            MusicToggled?.Invoke();
            _saveLoadService.SaveAudioSettings(_audioSettings);
        }

        public void TurnOnSfx()
        {
            _audioSettings.SfxEnabled = true;
            SfxToggled?.Invoke();
            _saveLoadService.SaveAudioSettings(_audioSettings);
        }

        public void TurnOffSfx()
        {
            _audioSettings.SfxEnabled = false;
            SfxToggled?.Invoke();
            _saveLoadService.SaveAudioSettings(_audioSettings);
        }
    }
}