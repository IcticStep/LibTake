using System;
using Code.Runtime.Services.Player.Inventory;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Ui.HudComponents.MainPanel
{
    internal sealed class CoinsView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private RectTransform _animationTarget;
        
        private IPlayerInventoryService _playerInventoryService;

        [Inject]
        private void Construct(IPlayerInventoryService playerInventoryService) =>
            _playerInventoryService = playerInventoryService;

        private void OnValidate() =>
            _animationTarget ??= GetComponent<RectTransform>();

        private void Awake()
        {
            _playerInventoryService.CoinsUpdated += UpdateView;
            UpdateView();
        }

        private void OnDestroy() =>
            _playerInventoryService.CoinsUpdated -= UpdateView;

        private void UpdateView()
        {
            _text.text = _playerInventoryService.Coins.ToString();
            _animationTarget
                .DOPunchScale(Vector3.one * 1.1f, 0.5f, 1, 0.5f)
                .ToUniTask(cancellationToken: this.GetCancellationTokenOnDestroy());
        }
    }
}