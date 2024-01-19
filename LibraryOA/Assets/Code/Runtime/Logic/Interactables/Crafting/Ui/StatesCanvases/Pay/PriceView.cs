using TMPro;
using UnityEngine;

namespace Code.Runtime.Logic.Interactables.Crafting.Ui.StatesCanvases.Pay
{
    internal sealed class PriceView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _priceText;

        [SerializeField]
        private PayStateCanvasView _payStateCanvasView;

        private void Awake() =>
            _payStateCanvasView.CoinPriceUpdated += UpdatePrice;

        private void OnDestroy() =>
            _payStateCanvasView.CoinPriceUpdated -= UpdatePrice;

        private void UpdatePrice(int price) =>
            _priceText.text = "x" + price;
    }
}