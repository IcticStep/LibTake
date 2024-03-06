using Code.Runtime.Logic.Audio;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Trucking
{
    internal sealed class TruckAudio : MonoBehaviour
    {
        [SerializeField]
        private Truck _truck;
        [SerializeField]
        private AudioClip _booksTakenSound;
        
        private AudioPlayer _audioPlayer;

        [Inject]
        private void Construct(AudioPlayer audioPlayer) =>
            _audioPlayer = audioPlayer;

        private void Awake() =>
            _truck.BooksTaken += OnBooksTaken;

        private void OnDestroy() =>
            _truck.BooksTaken -= OnBooksTaken;

        private void OnBooksTaken() =>
            _audioPlayer.PlaySfx(_booksTakenSound);
    }
}