using System;
using Code.Runtime.Logic.Audio;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.HudComponents.MainPanel.CustomersTimer
{
    internal sealed class CustomersTimerAudio : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _timerSound;
        [SerializeField]
        private CustomerTimerView _customerTimerView;
        
        private AudioPlayer _audioPlayer;

        [Inject]
        private void Construct(AudioPlayer audioPlayer) =>
            _audioPlayer = audioPlayer;

        private void Awake() =>
            _customerTimerView.ClockTick += OnTimerUpdated;

        private void OnDestroy() =>
            _customerTimerView.ClockTick -= OnTimerUpdated;

        private void OnTimerUpdated() =>
            _audioPlayer.PlaySfx(_timerSound);
    }
}