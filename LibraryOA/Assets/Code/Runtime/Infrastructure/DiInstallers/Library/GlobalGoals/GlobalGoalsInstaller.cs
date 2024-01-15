using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals
{
    internal sealed class GlobalGoalsInstaller : MonoInstaller, IInitializable
    {
        [SerializeField]
        private List<GlobalGoalVisualizationScheme> _globalGoalsVisualizationSchemes;

        public override void InstallBindings() =>
            Container.BindInterfacesAndSelfTo<GlobalGoalsInstaller>().FromInstance(this);

        public void Initialize()
        {
        }
    }
}