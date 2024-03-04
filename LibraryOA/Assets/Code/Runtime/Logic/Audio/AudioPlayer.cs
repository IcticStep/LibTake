using Code.Runtime.Infrastructure.Settings;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        [SerializeField]
        private AudioSource _ambientSource;
        
        private ISettingsService _settingsService;
        private Tweener _musicTweener;
        private Tweener _ambientTweener;

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

        public void FadeOutMusic(float duration)
        {
            KillMusicTweenerIfAny();
            _musicTweener = _musicSource
                .DOFade(0, duration)
                .SetEase(Ease.OutQuad)
                .OnComplete(KillMusicTweenerIfAny);
        }

        public void FadeInMusic(float duration)
        {
            KillMusicTweenerIfAny();
            _musicTweener = _musicSource
                .DOFade(1, duration)
                .SetEase(Ease.OutQuad)
                .OnComplete(KillMusicTweenerIfAny);
        }

        public void FadeOutAmbient(float duration)
        {
            KillAmbientTweenerIfAny();
            _ambientTweener = _ambientSource
                .DOFade(0, duration)
                .SetEase(Ease.OutQuad)
                .OnComplete(KillAmbientTweenerIfAny);
        }

        public void FadeInAmbient(float duration)
        {
            KillAmbientTweenerIfAny();
            _ambientTweener = _ambientSource
                .DOFade(1, duration)
                .SetEase(Ease.OutQuad)
                .OnComplete(KillAmbientTweenerIfAny);
        }

        public void StartAmbientIfNot()
        {
            if(_ambientSource.isPlaying)
                return;
            _ambientSource.Play();
        }

        public void StopAmbient() =>
            _ambientSource.Stop();
        
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
        
        private void KillMusicTweenerIfAny() =>
            KillTweenerIfAny(ref _musicTweener);
        
        private void KillAmbientTweenerIfAny() =>
            KillTweenerIfAny(ref _ambientTweener);
        
        private void KillTweenerIfAny(ref Tweener tweener)
        {
            tweener?.Kill();
            tweener = null;
        }
    }
}