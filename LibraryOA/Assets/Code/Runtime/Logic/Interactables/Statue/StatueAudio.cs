using System;
using Code.Runtime.Logic.Audio;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Statue
{
    internal sealed class StatueAudio : MonoBehaviour
    {
        [SerializeField]
        private Statue _statue;
        
        [SerializeField]
        private AudioClip _buyingSound;
        
        private AudioPlayer _audioPlayer;

        [Inject]
        private void Construct(AudioPlayer audioPlayer) =>
            _audioPlayer = audioPlayer;

        private void Awake() =>
            _statue.MoneySpent += OnMoneySpent;

        private void OnDestroy() =>
            _statue.MoneySpent -= OnMoneySpent;

        private void OnMoneySpent(int count) =>
            _audioPlayer.PlaySfx(_buyingSound);
    }
}