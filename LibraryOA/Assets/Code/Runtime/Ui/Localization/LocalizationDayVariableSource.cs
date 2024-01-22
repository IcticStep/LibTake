using Code.Runtime.Services.Days;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.Localization
{
    internal sealed class LocalizationDayVariableSource : MonoBehaviour
    {
        private IDaysService _daysService;
        
        public int CurrentDay { get; private set; }

        [Inject]
        private void Construct(IDaysService daysService)
        {
            _daysService = daysService;
            UpdateDaysVariable();
        }

        private void Awake() =>
            _daysService.Updated += UpdateDaysVariable;

        private void OnDestroy() =>
            _daysService.Updated -= UpdateDaysVariable;

        private void UpdateDaysVariable() =>
            CurrentDay = _daysService.CurrentDay+1;
    }
}