using System;
using Code.Runtime.Data;
using Code.Runtime.Logic.Audio;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Crafting.Tools.Hammer
{
    internal sealed class HammerAudio : MonoBehaviour
    {
        [SerializeField]
        private HammerCoddedAnimation _hammerCoddedAnimation;
        [SerializeField]
        private AudioClip[] _hammerSounds;
        
        private AudioPlayer _audioPlayer;

        [Inject]
        private void Construct(AudioPlayer audioPlayer) =>
            _audioPlayer = audioPlayer;

        private void Awake() =>
            _hammerCoddedAnimation.MovingIn += OnMoveIn;
        
        private void OnDestroy() =>
            _hammerCoddedAnimation.MovingIn -= OnMoveIn;

        private void OnMoveIn() =>
            _audioPlayer.PlaySfx(_hammerSounds.RandomElement());
    }
}