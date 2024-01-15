using System.Collections.Generic;
using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals.Data;
using Zenject;

namespace Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals
{
    public sealed class GlobalGoalsInstaller : MonoInstaller, IInitializable
    {
        public List<GlobalGoalScheme> GlobalGoalsVisualizationSchemes;

        public override void InstallBindings() =>
            Container.BindInterfacesAndSelfTo<GlobalGoalsInstaller>().FromInstance(this);

        public void Initialize()
        {
        }
    }
}