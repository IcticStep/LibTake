using System;
using System.Collections.Generic;
using Code.Runtime.StaticData.GlobalGoals;
using UnityEngine;

namespace Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals.Data
{
    [Serializable]
    public sealed class GlobalGoalScheme
    {
        [SerializeField]
        private GlobalGoal _globalGoal;

        [SerializeField]
        private List<GlobalStepScheme> _globalStepsSchemes;
        
        public GlobalGoal Goal => _globalGoal;
        public IReadOnlyList<GlobalStepScheme> GlobalStepsSchemes => _globalStepsSchemes;
        
        public GlobalGoalScheme(GlobalGoal globalGoal, List<GlobalStepScheme> globalStepsSchemes)
        {
            _globalGoal = globalGoal;
            _globalStepsSchemes = globalStepsSchemes;
        }

        public override string ToString() =>
            $"{nameof(GlobalGoalScheme)} for goal {_globalGoal.name}";
    }
}