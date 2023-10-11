using Code.Runtime.Services.Player;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Player
{
    internal sealed class PlayerInventoryView : MonoBehaviour
    {
        [SerializeField] private GameObject _bookObject;
        private IPlayerInventoryService _playerInventoryService;
 
        [Inject]
        private void Construct(IPlayerInventoryService playerInventoryService) =>
            _playerInventoryService = playerInventoryService;

        private void Awake() =>
            _playerInventoryService.Updated += UpdateView;

        private void Start() =>
            UpdateView();

        private void OnDestroy() =>
            _playerInventoryService.Updated -= UpdateView;

        private void UpdateView() =>
            _bookObject.SetActive(_playerInventoryService.HasBook);
    }
}