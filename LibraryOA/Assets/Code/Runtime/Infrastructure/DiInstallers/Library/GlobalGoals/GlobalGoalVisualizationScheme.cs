using System;
using System.Collections.Generic;
using Code.Runtime.Logic.GlobalProgress;
using Code.Runtime.StaticData.GlobalGoals;
using UnityEngine;

namespace Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals
{
    [Serializable]
    public sealed class GlobalGoalVisualizationScheme
    {
        [SerializeField]
        private GlobalGoal _globalGoal;

        [SerializeField]
        private List<GlobalStepPartVisualizer> _globalPartsSwitchers;
        
        public GlobalGoal Goal => _globalGoal;
        public IReadOnlyList<GlobalStepPartVisualizer> GlobalPartsSwitchers => _globalPartsSwitchers;

        public GlobalGoalVisualizationScheme(GlobalGoal globalGoal, List<GlobalStepPartVisualizer> globalStepPartVisualizers) 
        {
            _globalGoal = globalGoal;
            _globalPartsSwitchers = globalStepPartVisualizers;
        }
    }
}