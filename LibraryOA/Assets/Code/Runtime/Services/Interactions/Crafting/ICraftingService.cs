using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.StaticData.GlobalGoals;

namespace Code.Runtime.Services.Interactions.Crafting
{
    public interface ICraftingService : ISavedProgress
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