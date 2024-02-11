using Code.Runtime.Logic.GlobalGoals.RocketStart;

namespace Code.Runtime.Services.GlobalRocket
{
    internal interface IRocketProvider
    {
        Rocket Rocket { get; }
        void RegisterRocket(Rocket rocket);
        void CleanUp();
    }
}