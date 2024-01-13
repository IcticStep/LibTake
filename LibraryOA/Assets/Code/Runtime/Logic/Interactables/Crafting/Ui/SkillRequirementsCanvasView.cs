using System;
using System.Collections.Generic;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.StaticData.GlobalGoals;
using UnityEngine;

namespace Code.Runtime.Logic.Interactables.Crafting.Ui
{
    internal sealed class SkillRequirementsCanvasView : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;
        [SerializeField]
        private CraftingTableStateMachine _craftingTableStateMachine;
        
        public event Action<IReadOnlyList<SkillConstraint>> RequirementsUpdated; 

        private void Awake()
        {
            HideCanvas();
            _craftingTableStateMachine.EnterState += OnStateEnter;
            _craftingTableStateMachine.ExitState += OnStateExit;
        }
        
        private void OnDestroy()
        {
            _craftingTableStateMachine.EnterState -= OnStateEnter;
            _craftingTableStateMachine.ExitState -= OnStateExit;
        }

        private void OnStateEnter(ICraftingTableState state)
        {
            if(state is not SkillCheckState skillCheckState)
                return;
            
            RequirementsUpdated?.Invoke(skillCheckState.SkillRequirements);
            ShowCanvas();
        }

        private void OnStateExit(ICraftingTableState state)
        {
            if(state is not SkillCheckState skillCheckState)
                return;
            
            HideCanvas();
        }

        private void HideCanvas() =>
            _canvas.enabled = false;
        
        private void ShowCanvas() =>
            _canvas.enabled = true;
    }
}