using Code.Runtime.Services.Player.Lives;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.Hud.MainPanel
{
    internal sealed class LivesView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        
        private IPlayerLivesService _livesService;

        [Inject]
        private void Construct(IPlayerLivesService livesService) =>
            _livesService = livesService;

        private void Start()
        {
            _livesService.Updated += UpdateView;
            UpdateView();
        }

        private void OnDestroy() =>
            _livesService.Updated -= UpdateView;

        private void UpdateView() =>
            _text.text = _livesService.Lives.ToString();
    }
}