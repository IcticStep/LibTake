using Code.Runtime.Logic.Triggers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Logic.Audio
{
    internal sealed class StopAmbientOnTrigger : MonoBehaviour
    {
        [SerializeField]
        private Trigger _trigger;
        [SerializeField]
        private float _musicFadeInSeconds;
        [SerializeField]
        private float _ambientFadeOutSeconds;

        private AudioPlayer _audioPlayer;

        [Inject]
        private void Construct(AudioPlayer audioPlayer) =>
            _audioPlayer = audioPlayer;

        private void Awake() =>
            _trigger.Entered += OnTrigger;

        private void OnDestroy() =>
            _trigger.Entered -= OnTrigger;

        private void OnTrigger()
        {
            _audioPlayer.FadeInMusic(_musicFadeInSeconds);
            _audioPlayer.FadeOutAmbient(_ambientFadeOutSeconds);
        }
    }
}