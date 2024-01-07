using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Code.Runtime.Ui.Coin
{
    internal sealed class FlyingCoin : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        [SerializeField]
        private MoverY _moverY;
        [SerializeField]
        private TextMeshProUGUI _coinsAmountText;
        
        private void Start() =>
            _smoothFader.FadeImmediately();
        
        public async UniTask ShowCoin(int amount)
        {
            _moverY.Move();
            _coinsAmountText.text = "+" + amount;
            await _smoothFader.UnFadeAsync();
            // ReSharper disable once MethodHasAsyncOverload
            _smoothFader.Fade();
        }
    }
}