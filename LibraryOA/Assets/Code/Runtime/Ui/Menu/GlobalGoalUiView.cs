using Code.Runtime.StaticData.GlobalGoals;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.Ui.Menu
{
    internal sealed class GlobalGoalUiView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TextMeshProUGUI _header;
        [SerializeField]
        private Button _playButton;
        [SerializeField]
        private GlobalGoal _goalView;
        [SerializeField]
        private GlobalGoalsContainer _globalGoalsContainer;
        
        private void Awake()
        {
            _icon.sprite = _goalView.Icon;
            _header.text = _goalView.LocalizedName.GetLocalizedString();
            _playButton.onClick.AddListener(OnPlayButton);
        }

        private void OnDestroy() =>
            _playButton.onClick.RemoveListener(OnPlayButton);

        private void OnPlayButton() =>
            StartNewGame()
                .Forget();

        private async UniTask StartNewGame() =>
            await _globalGoalsContainer.Hide();
    }
}