using Code.Runtime.Data;
using Code.Runtime.Infrastructure.GameStates;
using Code.Runtime.Infrastructure.GameStates.States;
using Code.Runtime.Ui.Menu.Common;
using Code.Runtime.Ui.Menu.GlobalGoals;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui.Menu.MainButtons
{
    internal sealed class NewGameButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private GlobalGoalsContainer _globalsGoalsContainer;
        [SerializeField]
        private MenuGroup _mainButtonsGroup;
        [SerializeField]
        private GameName _gameName;

        private void Awake() =>
            _button.onClick.AddListener(OnNewGameButton);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(OnNewGameButton);

        private void OnNewGameButton() =>
            ShowGoalsSelection()
                .Forget();

        private async UniTaskVoid ShowGoalsSelection()
        {
            _gameName.Hide().Forget();
            _globalsGoalsContainer.Show().Forget();
            await _mainButtonsGroup.Hide();
        }
    }
}