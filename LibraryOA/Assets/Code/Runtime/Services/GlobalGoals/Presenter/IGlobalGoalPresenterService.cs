using Code.Runtime.StaticData.GlobalGoals;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Services.GlobalGoals.Presenter
{
    internal interface IGlobalGoalPresenterService
    {
        UniTaskVoid ShowBuiltStep(GlobalStep globalStep, GlobalGoal globalGoal);
    }
}