using Code.Runtime.Services.GlobalGoals.Visualization;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.StaticData.GlobalGoals;

namespace Code.Runtime.Services.GlobalGoals
{
    internal interface IGlobalGoalService
    {
        void SetGlobalGoal(GlobalGoal globalGoal);
    }
}