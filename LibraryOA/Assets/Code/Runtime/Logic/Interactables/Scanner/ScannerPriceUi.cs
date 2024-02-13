using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Ui.Common;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Scanner
{
    internal sealed class ScannerPriceUi : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _priceText;
        [SerializeField]
        private HoverView _hoverView;
        [SerializeField]
        private SmoothFader _smoothFader;
        
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        private void Start()
        {
            _priceText.text = "x" + _staticDataService.Interactables.Scanner.CoinsReward;
            _hoverView.Focused += OnHoverFocused;
            _hoverView.Unfocused += OnHoverUnfocused;
        }

        private void OnDestroy()
        {
            _hoverView.Focused -= OnHoverFocused;
            _hoverView.Unfocused -= OnHoverUnfocused;
        }

        private void OnHoverFocused() =>
            _smoothFader.UnFade();

        private void OnHoverUnfocused() =>
            _smoothFader.Fade();
    }
}