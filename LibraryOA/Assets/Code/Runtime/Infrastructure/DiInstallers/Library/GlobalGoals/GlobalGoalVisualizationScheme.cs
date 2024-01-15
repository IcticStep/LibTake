using System;
using System.Collections.Generic;
using Code.Runtime.Logic.GlobalProgress;
using Code.Runtime.StaticData.GlobalGoals;
using UnityEngine;

namespace Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals
{
    [Serializable]
    internal sealed class GlobalGoalVisualizationScheme
    {
        [SerializeField]
        private GlobalGoal _globalGoal;

        [SerializeField]
        private List<GlobalStepPartVisualizer> _globalPartsSwitchers = new();
        
        public GlobalGoal Goal => _globalGoal;
        public IReadOnlyList<GlobalStepPartVisualizer> GlobalPartsSwitchers => _globalPartsSwitchers;
    }
}