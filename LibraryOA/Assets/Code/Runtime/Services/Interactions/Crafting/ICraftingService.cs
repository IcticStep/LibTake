using Code.Runtime.StaticData.GlobalGoals;

namespace Code.Runtime.Services.Interactions.Crafting
{
    internal interface ICraftingService
    {
        GlobalGoal Goal { get; }
        int CurrentStepIndex { get; }
        GlobalStep CurrentStep { get; }
        bool FinishedGoal { get; }
        bool PayedForStep { get; }
        void SetGoal(GlobalGoal globalGoal);
        void CraftStep();
        bool CanCraftStep();
        bool CanPayForStep();
        bool HaveEnoughSkillsToCraft();
        int PayForStep();
    }
}