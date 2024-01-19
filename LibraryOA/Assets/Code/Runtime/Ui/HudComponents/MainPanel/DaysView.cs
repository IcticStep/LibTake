using Code.Runtime.Services.Days;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.HudComponents.MainPanel
{
    internal sealed class DaysView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        
        private IDaysService _daysService;

        [Inject]
        private void Construct(IDaysService daysService) =>
            _daysService = daysService;

        private void Start()
        {
            _daysService.Updated += UpdateView;
            UpdateView();
        }

        private void OnDestroy() =>
            _daysService.Updated -= UpdateView;

        private void UpdateView() =>
            _text.text = _daysService.CurrentDay.ToString();
    }
}