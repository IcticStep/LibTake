using System.Collections.Generic;
using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals.Data;
using Code.Runtime.Services.GlobalGoals.Visualization;
using Code.Runtime.Services.Interactions.Crafting;
using Zenject;

namespace Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals
{
    public sealed class GlobalGoalsInstaller : MonoInstaller, IInitializable
    {
        public List<GlobalGoalScheme> GlobalGoalsVisualizationSchemes;
        private IGlobalGoalsVisualizationService _globalGoalsVisualizationService;

        [Inject]
        private void Construct(IGlobalGoalsVisualizationService globalGoalsVisualizationService, ICraftingService craftingService) =>
            _globalGoalsVisualizationService = globalGoalsVisualizationService;

        public override void InstallBindings() =>
            Container.BindInterfacesAndSelfTo<GlobalGoalsInstaller>().FromInstance(this);

        public void Initialize()
        {
        }
    }
}