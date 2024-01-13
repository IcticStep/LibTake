using System;
using System.Collections.Generic;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates;
using Code.Runtime.StaticData.GlobalGoals;

namespace Code.Runtime.Logic.Interactables.Crafting.Ui.StatesCanvases.SkillRequirementCheck
{
    internal sealed class SkillRequirementsCanvasView : CanvasViewForState<SkillCheckState>
    {
        public event Action<IReadOnlyList<SkillConstraint>> RequirementsUpdated;

        protected override void OnCanvasShow(SkillCheckState state) =>
            RequirementsUpdated?.Invoke(state.SkillRequirements);
    }
}