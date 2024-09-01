using Code.Runtime.Services.Player.Lives;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.HudComponents.MainPanel
{
    internal sealed class LivesView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private RectTransform _animationTarget;

        private IPlayerLivesService _livesService;

        [Inject]
        private void Construct(IPlayerLivesService livesService) =>
            _livesService = livesService;

        private void OnValidate() =>
            _animationTarget ??= GetComponent<RectTransform>();
        
        private void Start()
        {
            _livesService.Updated += UpdateView;
            UpdateView();
        }

        private void OnDestroy() =>
            _livesService.Updated -= UpdateView;

        private void UpdateView()
        {
            _text.text = _livesService.Lives.ToString();
            _animationTarget
                .DOPunchScale(Vector3.one * 1.1f, 0.5f, 1, 0.5f)
                .SetLink(gameObject);
        }
    }
}