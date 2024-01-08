using System;
using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.Runtime.Ui.Coin
{
    internal sealed class FlyingResource : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        [SerializeField]
        private MoverY _moverY;
        [SerializeField]
        private TextMeshProUGUI _coinsAmountText;
        [SerializeField]
        private Image _image;
        [SerializeField]
        private Sprite _sprite;
        
        private void Start()
        {
            _smoothFader.FadeImmediately();
            _image.sprite = _sprite;
        }

        private void OnValidate()
        {
            _smoothFader ??= GetComponent<SmoothFader>();
            _moverY ??= GetComponent<MoverY>();
            _image ??= GetComponent<Image>();
            _sprite ??= _image.sprite;
        }

        public async UniTask FlyResource(int amount)
        {
            _moverY.Move();
            _coinsAmountText.text = "+" + amount;
            await _smoothFader.UnFadeAsync();
            // ReSharper disable once MethodHasAsyncOverload
            _smoothFader.Fade();
        }
    }
}