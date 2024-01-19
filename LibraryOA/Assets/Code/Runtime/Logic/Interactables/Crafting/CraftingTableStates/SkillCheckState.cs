using System;
using System.Collections.Generic;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.StaticData.GlobalGoals;

namespace Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates
{
    internal sealed class SkillCheckState : ICraftingTableState
    {
        private readonly CraftingTableStateMachine _craftingTableStateMachine;
        private readonly ICraftingService _craftingService;

        public IReadOnlyList<SkillConstraint> SkillRequirements => _craftingService.FinishedGoal
            ? Array.Empty<SkillConstraint>()
            : _craftingService.CurrentStep.SkillRequirements;

        public SkillCheckState(CraftingTableStateMachine craftingTableStateMachine, ICraftingService craftingService)
        {
            _craftingTableStateMachine = craftingTableStateMachine;
            _craftingService = craftingService;
        }

        public bool CanInteract() =>
            _craftingService.HaveEnoughSkillsToCraft();

        public void Interact() =>
            _craftingTableStateMachine.Enter<CraftingState>();
    }
}