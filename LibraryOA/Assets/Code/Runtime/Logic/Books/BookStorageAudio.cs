using Code.Runtime.Data;
using Code.Runtime.Logic.Audio;
using Code.Runtime.Logic.Interactables;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Books
{
    internal sealed class BookStorageAudio : MonoBehaviour
    {
        [SerializeField]
        private BookStorage _bookStorage;
        [SerializeField]
        private AudioClip[] _bookRemovedSounds;
        [SerializeField]
        private AudioClip[] _bookInsertedSound;
        
        private AudioPlayer _audioPlayer;

        [Inject]
        private void Construct(AudioPlayer audioPlayer) =>
            _audioPlayer = audioPlayer;

        private void Start()
        {
            _bookStorage.BookInserted += OnBookInserted;
            _bookStorage.BookRemoved += OnBookRemoved;
        }

        private void OnDestroy()
        {
            _bookStorage.BookInserted -= OnBookInserted;
            _bookStorage.BookRemoved -= OnBookRemoved;
        }

        private void OnBookInserted() =>
            _audioPlayer.PlaySfx(_bookInsertedSound.RandomElement());

        private void OnBookRemoved() =>
            _audioPlayer.PlaySfx(_bookRemovedSounds.RandomElement());
    }
}