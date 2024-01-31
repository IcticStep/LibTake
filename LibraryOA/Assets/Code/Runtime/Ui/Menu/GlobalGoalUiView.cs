using Code.Runtime.Data;
using Code.Runtime.Infrastructure.GameStates;
using Code.Runtime.Infrastructure.GameStates.States;
using Code.Runtime.Services.GlobalGoals;
using Code.Runtime.Services.Loading;
using Code.Runtime.StaticData.GlobalGoals;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
        
        private IGlobalGoalService _globalGoalService;
        private ILoadingCurtainService _loadingCurtainService;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGlobalGoalService globalGoalService, ILoadingCurtainService loadingCurtainService, GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _loadingCurtainService = loadingCurtainService;
            _globalGoalService = globalGoalService;
        }

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

        private async UniTask StartNewGame()
        {
            _globalGoalService.SetGlobalGoal(_goalView);
            _globalGoalsContainer.Hide().Forget();
            _loadingCurtainService.Show();
            await UniTask.WaitForSeconds(_globalGoalsContainer.Duration / 2);
            _gameStateMachine.EnterState<LoadProgressState, LoadProgressOption>(LoadProgressOption.NewGame);
        }
    }
}