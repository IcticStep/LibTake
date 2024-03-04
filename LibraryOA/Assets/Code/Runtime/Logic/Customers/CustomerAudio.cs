using System;
using Code.Runtime.Logic.Audio;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Customers
{
    internal sealed class CustomerAudio : MonoBehaviour
    {
        [SerializeField]
        private CustomerStateMachine _customerStateMachine;
        [SerializeField]
        private AudioClip _customerFailedSound;
        [SerializeField]
        private AudioClip _customerRewardSound;
        
        private AudioPlayer _audioPlayer;

        [Inject]
        private void Construct(AudioPlayer audioPlayer) =>
            _audioPlayer = audioPlayer;

        private void Awake()
        {
            _customerStateMachine.CustomerFailed += OnCustomerFailed;
            _customerStateMachine.CustomerReward += OnCustomerReward;
        }

        private void OnDestroy()
        {
            _customerStateMachine.CustomerFailed -= OnCustomerFailed;
            _customerStateMachine.CustomerReward -= OnCustomerReward;
        }

        private void OnCustomerFailed() =>
            _audioPlayer.PlaySfx(_customerFailedSound);
        
        private void OnCustomerReward() =>
            _audioPlayer.PlaySfx(_customerRewardSound);
    }
}