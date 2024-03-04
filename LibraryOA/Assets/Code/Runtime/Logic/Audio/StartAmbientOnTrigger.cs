using Code.Runtime.Logic.Triggers;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Audio
{
    internal sealed class StartAmbientOnTrigger : MonoBehaviour
    {
        [SerializeField]
        private Trigger _trigger;
        [SerializeField]
        private float _musicFadeInSeconds = 0.5f;
        [SerializeField]
        private float _ambientFadeInSeconds = 0.5f;

        private AudioPlayer _audioPlayer;

        [Inject]
        private void Construct(AudioPlayer audioPlayer) =>
            _audioPlayer = audioPlayer;

        private void Awake() =>
            _trigger.Entered += StartAmbient;

        private void OnDestroy() =>
            _trigger.Entered -= StartAmbient;

        private void StartAmbient()
        {
            _audioPlayer.FadeOutMusic(_musicFadeInSeconds);
            _audioPlayer.StartAmbientIfNot();
            _audioPlayer.FadeInAmbient(_ambientFadeInSeconds);
        }
    }
}