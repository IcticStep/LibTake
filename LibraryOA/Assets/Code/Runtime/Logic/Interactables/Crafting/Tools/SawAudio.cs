using Code.Runtime.Data;
using Code.Runtime.Logic.Animations.Implementations.Crafting;
using Code.Runtime.Logic.Audio;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Crafting.Tools
{
    internal sealed class SawAudio : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] _sawMoveOutSounds;
        [SerializeField]
        private AudioClip[] _sawMoveInSounds;
        [SerializeField]
        private CraftingSawCoddedAnimation _sawCoddedAnimation;
        
        private AudioPlayer _audioPlayer;

        [Inject]
        private void Construct(AudioPlayer audioPlayer) =>
            _audioPlayer = audioPlayer;

        private void Awake()
        {
            _sawCoddedAnimation.MovingIn += OnMoveIn;
            _sawCoddedAnimation.MovingOut += OnMoveOut;
        }

        private void OnDestroy()
        {
            _sawCoddedAnimation.MovingIn -= OnMoveIn;
            _sawCoddedAnimation.MovingOut -= OnMoveOut;
        }

        private void OnMoveIn() =>
            _audioPlayer.PlaySfx(_sawMoveInSounds.RandomElement());

        private void OnMoveOut() =>
            _audioPlayer.PlaySfx(_sawMoveOutSounds.RandomElement());
    }
}