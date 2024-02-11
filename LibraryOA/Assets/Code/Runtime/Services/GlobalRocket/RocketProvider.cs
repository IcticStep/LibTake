using Code.Runtime.Logic.GlobalGoals.RocketStart;
using JetBrains.Annotations;

namespace Code.Runtime.Services.GlobalRocket
{
    [UsedImplicitly]
    internal sealed class RocketProvider : IRocketProvider
    {
        public Rocket Rocket { get; private set; }

        public void RegisterRocket(Rocket rocket) =>
            Rocket = rocket;

        public void CleanUp() =>
            Rocket = null;
    }
}