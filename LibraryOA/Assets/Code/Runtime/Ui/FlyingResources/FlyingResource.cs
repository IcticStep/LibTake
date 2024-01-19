using System;
using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.Ui.FlyingResources
{
    internal sealed class FlyingResource : MonoBehaviour
    {
        private const string PositivePrefix = "+";
        private const string NegativePrefix = "";
        
        [SerializeField]
        private SmoothFader _smoothFader;
        [SerializeField]
        private MoverY _moverY;
        [SerializeField]
        private TextMeshProUGUI _coinsAmountText;
        [SerializeField]
        private Image _image;

        private void Start() =>
            _smoothFader.FadeImmediately();

        private void OnValidate()
        {
            _smoothFader ??= GetComponent<SmoothFader>();
            _moverY ??= GetComponent<MoverY>();
        }

        public async UniTask FlyResource(int amount)
        {
            _moverY.Move();
            string prefix = GetPrefix(amount); 
            _coinsAmountText.text = prefix + amount;
            await _smoothFader.UnFadeAsync();
            // ReSharper disable once MethodHasAsyncOverload
            _smoothFader.Fade();
        }

        public async UniTask FlyResource(Sprite icon, int amount)
        {
            _image.sprite = icon;
            await FlyResource(amount);
        }

        private static string GetPrefix(int amount) =>
            amount >= 0 ? PositivePrefix : NegativePrefix;
    }
}