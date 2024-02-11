using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.StaticData.GlobalGoals;

namespace Code.Runtime.Services.GlobalGoals
{
    internal interface IGlobalGoalService : ISavedProgress
    {
        GlobalGoal GlobalGoal { get; }
        void SetGlobalGoal(GlobalGoal globalGoal);
    }
}