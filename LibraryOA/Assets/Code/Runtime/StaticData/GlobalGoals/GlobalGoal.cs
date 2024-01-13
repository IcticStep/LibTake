using System;
using System.Collections.Generic;
using Code.Runtime.Data;
using UnityEngine;

namespace Code.Runtime.StaticData.GlobalGoals
{
    [CreateAssetMenu(fileName = "Global Goal", menuName = "Static data/Global Goals/Global Goal", order = 0)]
    public class GlobalGoal : ScriptableObject
    {
        [SerializeField]
        private string _name;
        
        [ReadOnly]
        [SerializeField]
        private string _uniqueId = Guid.NewGuid().ToString();
        
        [SerializeField]
        private List<GlobalStep> _globalSteps;

        [SerializeField]
        private Sprite _icon;
        
        public string Name => _name;
        public IReadOnlyList<GlobalStep> GlobalSteps => _globalSteps;
        public string UniqueId => _uniqueId;
        public Sprite Icon => _icon;

        private void OnValidate() =>
            _uniqueId = UniqueId ?? Guid.NewGuid().ToString();
    }
}