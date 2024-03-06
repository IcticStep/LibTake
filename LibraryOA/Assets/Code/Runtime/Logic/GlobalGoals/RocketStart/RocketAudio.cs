using Code.Runtime.Logic.Audio;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.GlobalGoals.RocketStart
{
    internal sealed class RocketAudio : MonoBehaviour
    {
        [SerializeField]
        private Rocket _rocket;
        [SerializeField]
        private AudioClip _rocketSound;
        
        private AudioPlayer _audioPlayer;

        [Inject]
        private void Construct(AudioPlayer audioPlayer) =>
            _audioPlayer = audioPlayer;

        private void Awake() =>
            _rocket.RocketLaunch += OnRocketLaunch;

        private void OnDestroy() =>
            _rocket.RocketLaunch -= OnRocketLaunch;

        private void OnRocketLaunch() =>
            _audioPlayer.PlaySfx(_rocketSound);
    }
}