using Code.Runtime.Logic.Audio;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.GlobalGoals.Presenter;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Crafting
{
    internal sealed class CraftingTableAudio : MonoBehaviour
    {
        [SerializeField]
        private CraftingTableStateMachine _craftingTableStateMachine;

        [SerializeField]
        private AudioClip _buyingSound;
        [FormerlySerializedAs("_successSound")]
        [SerializeField]
        private AudioClip _successCraftingStepSound;
        [SerializeField]
        private AudioClip _successGlobalStepSound;
        
        private AudioPlayer _audioPlayer;
        private IGlobalGoalPresenterService _globalGoalPresenterService;

        [Inject]
        private void Construct(AudioPlayer audioPlayer, IGlobalGoalPresenterService globalGoalPresenterService)
        {
            _globalGoalPresenterService = globalGoalPresenterService;
            _audioPlayer = audioPlayer;
        }

        private void Start()
        {
            _craftingTableStateMachine.ExitState += OnExitState;
            _globalGoalPresenterService.GlobalStepCompleted += OnCameraArrivedOnGlobalStepObject;
        }

        private void OnDestroy()
        {
            _craftingTableStateMachine.ExitState -= OnExitState;
            _globalGoalPresenterService.GlobalStepCompleted -= OnCameraArrivedOnGlobalStepObject;
        }

        private void OnExitState(ICraftingTableState state)
        {
            if(state is PayState)
                PlayBuyingSound();
            else
                PlaySuccessSound();
        }

        private void PlayBuyingSound() =>
            _audioPlayer.PlaySfx(_buyingSound);

        private void PlaySuccessSound() =>
            _audioPlayer.PlaySfx(_successCraftingStepSound);

        private void OnCameraArrivedOnGlobalStepObject() =>
            _audioPlayer.PlaySfx(_successGlobalStepSound);
    }
}