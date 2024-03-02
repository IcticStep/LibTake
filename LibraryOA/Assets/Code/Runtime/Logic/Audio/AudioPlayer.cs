using Code.Runtime.Infrastructure.Settings;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Audio
{
    internal sealed class AudioPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _musicSource;
        [SerializeField]
        private AudioSource _sfxSource;
        
        private ISettingsService _settingsService;

        [Inject]
        private void Construct(ISettingsService settingsService) =>
            _settingsService = settingsService;

        private void Awake() =>
            _settingsService.Updated += OnSettingsUpdated;

        private void OnDestroy() =>
            _settingsService.Updated -= OnSettingsUpdated;

        public void StopMusic() =>
            _musicSource.Pause();

        public void ResumeMusic() =>
            _musicSource.Play();

        public void StopSfx() =>
            _sfxSource.Stop();

        public void PlaySfx(AudioClip clip) =>
            _sfxSource.PlayOneShot(clip);

        private void OnSettingsUpdated()
        {
            if (_settingsService.MusicEnabled)
                ResumeMusic();
            else
                StopMusic();
            
            if (!_settingsService.SfxEnabled)
                StopSfx();
        }
    }
}