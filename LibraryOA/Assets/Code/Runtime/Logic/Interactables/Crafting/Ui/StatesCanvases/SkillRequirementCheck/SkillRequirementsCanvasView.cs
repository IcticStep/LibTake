using System;
using System.Collections.Generic;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates;
using Code.Runtime.StaticData.GlobalGoals;
using UnityEngine;

namespace Code.Runtime.Logic.Interactables.Crafting.Ui.StatesCanvases.SkillRequirementCheck
{
    internal sealed class SkillRequirementsCanvasView : CanvasViewForState<SkillCheckState>
    {
        [SerializeField]
        private CraftingTableStateMachine _craftingTable;
        
        public event Action<IReadOnlyList<SkillConstraint>> RequirementsRequested;

        protected override void OnAwaking() =>
            _craftingTable.Hovered += OnHovered;

        protected override void OnDestroying() =>
            _craftingTable.Hovered -= OnHovered;

        private void OnHovered()
        {
            if(_craftingTable.ActiveState is not SkillCheckState skillCheckState)
                return;
            
            RequirementsRequested?.Invoke(skillCheckState.SkillRequirements);
        }

        protected override void OnCanvasShow(SkillCheckState state) =>
            RequirementsRequested?.Invoke(state.SkillRequirements);
    }
}