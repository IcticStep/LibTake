using System;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.StaticData.GlobalGoals;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Services.Interactions.Crafting
{
    public interface ICraftingService : ISavedProgress
    {
        GlobalGoal Goal { get; }
        int CurrentStepIndex { get; }
        bool PayedForStep { get; }
        bool CraftingAllowed { get; }
        GlobalStep CurrentStep { get; }
        GlobalStep PreviousStep { get; }
        bool FinishedGoal { get; }
        event Action<bool> CraftingPermissionChanged;
        void SetGoal(GlobalGoal globalGoal);
        void AllowCrafting();
        void BlockCrafting();
        UniTask CraftStep();
        int PayForStep();
        bool CanCraftStep();
        bool CanPayForStep();
        bool HaveEnoughSkillsToCraft();
        void CleanUp();
    }
}