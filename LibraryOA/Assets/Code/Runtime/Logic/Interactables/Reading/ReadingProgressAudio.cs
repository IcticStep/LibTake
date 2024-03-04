using Code.Runtime.Data;
using Code.Runtime.Logic.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Reading
{
    [RequireComponent(typeof(Progress))]
    internal sealed class ReadingProgressAudio : MonoBehaviour
    {
        [SerializeField]
        private Progress _progress;
        [SerializeField]
        private AudioClip[] _readingSounds;
        [SerializeField]
        private AudioClip _readingFinishedSound;
        
        private AudioPlayer _audioPlayer;

        private bool _playingReadingSounds;

        [Inject]
        private void Construct(AudioPlayer audioPlayer) =>
            _audioPlayer = audioPlayer;

        private void Awake()
        {
            _progress.Started += OnReadingStarted;
            _progress.Stopped += OnReadingStopped;
            _progress.Finished += OnReadingFinished;
        }

        private void OnReadingStarted() =>
            PlayReadingSounds()
                .Forget();

        private async UniTaskVoid PlayReadingSounds()
        {
            if(_playingReadingSounds)
                return;
            
            do
            {
                _playingReadingSounds = true;
                await _audioPlayer.PlaySfxAsync(_readingSounds.RandomElement());
            } while(_progress.Running);
            
            _playingReadingSounds = false;
        }

        private void OnReadingStopped() =>
            _audioPlayer.PlaySfx(_readingFinishedSound);

        private void OnReadingFinished() =>
            _audioPlayer.PlaySfx(_readingFinishedSound);
    }
}