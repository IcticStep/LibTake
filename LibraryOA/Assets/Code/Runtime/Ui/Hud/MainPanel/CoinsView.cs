using Code.Runtime.Services.Player.Inventory;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.Hud.MainPanel
{
    internal sealed class CoinsView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        
        private IPlayerInventoryService _playerInventoryService;

        [Inject]
        private void Construct(IPlayerInventoryService playerInventoryService) =>
            _playerInventoryService = playerInventoryService;

        private void Awake()
        {
            _playerInventoryService.CoinsUpdated += UpdateView;
            UpdateView();
        }

        private void OnDestroy() =>
            _playerInventoryService.CoinsUpdated -= UpdateView;

        private void UpdateView() =>
            _text.text = _playerInventoryService.Coins.ToString();
    }
}