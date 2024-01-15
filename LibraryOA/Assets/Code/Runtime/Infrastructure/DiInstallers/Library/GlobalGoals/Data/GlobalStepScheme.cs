using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Logic.GlobalGoals;
using Code.Runtime.StaticData.GlobalGoals;
using UnityEngine;

namespace Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals.Data
{
    [Serializable]
    public sealed class GlobalStepScheme
    {
        [SerializeField]
        private GlobalStep _globalStep;
        
        [SerializeField]
        private List<GlobalStepPartVisualizer> _rootShowVisualizers;

        [SerializeField]
        private List<GlobalStepPartVisualizer> _allVisualizers;

        public List<GlobalStepPartVisualizer> AllVisualizers => _allVisualizers;
        public GlobalStep Step => _globalStep;

        public GlobalStepScheme(GlobalStep globalStep, List<GlobalStepPartVisualizer> allVisualizers)
        {
            _globalStep = globalStep;
            _allVisualizers = allVisualizers;
            _rootShowVisualizers = allVisualizers
                .Where(visualizer => visualizer.RootStepObject && visualizer.TargetStateAfterStep)
                .ToList();
        }
    }
}