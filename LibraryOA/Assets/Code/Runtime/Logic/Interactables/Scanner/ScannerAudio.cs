using Code.Runtime.Logic.Audio;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Scanner
{
    internal sealed class ScannerAudio : MonoBehaviour
    {
        [SerializeField]
        private ScannerCoddedAnimation _scannerCoddedAnimation;
        [SerializeField]
        private AudioClip _scannerSound;
        
        private AudioPlayer _audioPlayer;

        [Inject]
        private void Construct(AudioPlayer audioPlayer) =>
            _audioPlayer = audioPlayer;

        private void Awake() =>
            _scannerCoddedAnimation.ScanIteration += OnScanIteration;

        private void OnDestroy() =>
            _scannerCoddedAnimation.ScanIteration -= OnScanIteration;

        private void OnScanIteration() =>
            _audioPlayer.PlaySfx(_scannerSound);
    }
}