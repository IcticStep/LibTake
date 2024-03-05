using System;
using Code.Runtime.Logic.Audio;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Crafting
{
    internal sealed class CraftingTableAudio : MonoBehaviour
    {
        [SerializeField]
        private CraftingTableStateMachine _craftingTableStateMachine;

        [SerializeField]
        private AudioClip _buyingSound;
        
        private AudioPlayer _audioPlayer;

        [Inject]
        private void Construct(AudioPlayer audioPlayer) =>
            _audioPlayer = audioPlayer;

        private void Awake() =>
            _craftingTableStateMachine.ExitState += OnExitState;

        private void OnDestroy() =>
            _craftingTableStateMachine.ExitState -= OnExitState;

        private void OnExitState(ICraftingTableState state)
        {
            if(state is PayState)
                PlayBuyingSound();
        }

        private void PlayBuyingSound() =>
            _audioPlayer.PlaySfx(_buyingSound);
    }
}