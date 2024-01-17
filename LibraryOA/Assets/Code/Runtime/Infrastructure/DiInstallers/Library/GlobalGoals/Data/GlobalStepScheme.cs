using System;
using System.Collections.Generic;
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
        private GlobalStepPartVisualizer _cameraTarget;

        [SerializeField]
        private List<GlobalStepPartVisualizer> _visualizers;

        public List<GlobalStepPartVisualizer> Visualizers => _visualizers;
        public GlobalStep Step => _globalStep;
        public GlobalStepPartVisualizer CameraTarget => _cameraTarget;

        public GlobalStepScheme(GlobalStep globalStep, List<GlobalStepPartVisualizer> visualizers, GlobalStepPartVisualizer cameraTarget)
        {
            _globalStep = globalStep;
            _visualizers = visualizers;
            _cameraTarget = cameraTarget;
        }

        public override string ToString() =>
            $"{nameof(GlobalStepScheme)} for step {_globalStep.name}";
    }
}