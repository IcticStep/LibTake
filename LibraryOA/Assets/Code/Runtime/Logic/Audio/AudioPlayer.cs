using Code.Runtime.Infrastructure.Settings;
using Cysharp.Threading.Tasks;
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

        private void Awake()
        {
            _settingsService.SfxToggled += UpdateSfxState;
            _settingsService.MusicToggled += UpdateMusicState;
        }

        private void Start()
        {
            if(_settingsService.MusicEnabled)
                ResumeMusic();
        }

        private void OnDestroy()
        {
            _settingsService.SfxToggled -= UpdateSfxState;
            _settingsService.MusicToggled -= UpdateMusicState;
        }

        public void StopMusic() =>
            _musicSource.Pause();

        public void ResumeMusic() =>
            _musicSource.Play();

        public void StopSfx() =>
            _sfxSource.Stop();

        public void PlaySfx(AudioClip clip)
        {
            if(!_settingsService.SfxEnabled)
                return;
            
            _sfxSource.PlayOneShot(clip);
        }

        public UniTask PlaySfxAsync(AudioClip clip)
        {
            PlaySfx(clip);
            return UniTask.WaitForSeconds(clip.length);
        }

        private void UpdateMusicState()
        {
            if (_settingsService.MusicEnabled)
                ResumeMusic();
            else
                StopMusic();
        }

        private void UpdateSfxState()
        {
            if (!_settingsService.SfxEnabled)
                StopSfx();
        }
    }
}