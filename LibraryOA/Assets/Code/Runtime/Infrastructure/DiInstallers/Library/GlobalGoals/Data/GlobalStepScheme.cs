using System;
using System.Collections.Generic;
using Code.Runtime.Logic.GlobalProgress;
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
        private List<GlobalStepPartVisualizer> _visualizers;

        public List<GlobalStepPartVisualizer> Visualizers => _visualizers;
        public GlobalStep Step => _globalStep;

        public GlobalStepScheme(GlobalStep globalStep, List<GlobalStepPartVisualizer> visualizers)
        {
            _globalStep = globalStep;
            _visualizers = visualizers;
        }
    }
}