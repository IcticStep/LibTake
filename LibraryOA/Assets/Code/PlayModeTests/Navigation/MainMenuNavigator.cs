using System.Linq;
using System.Threading.Tasks;
using Code.PlayModeTests.Extensions;
using Code.Runtime.Ui.Menu.GlobalGoals;
using Code.Runtime.Ui.Menu.MainButtons;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.PlayModeTests.Navigation
{
    internal static class MainMenuNavigator
    {
        private const float AfterStepDelay = 2f;

        public static async Task OpenMainMenu()
        {
            await SceneManager.LoadSceneAsync(0);
            await UniTask.WaitForSeconds(AfterStepDelay);
        }

        public static async Task StartNewGame()
        {
            FindNewGameButton().SimulateClick();
            await UniTask.WaitForSeconds(AfterStepDelay);
        }

        public static async Task StartFirstGlobalGoal()
        {
            FindGlobalGoalButton().SimulateClick();
            await UniTask.WaitForSeconds(AfterStepDelay);
        }

        private static Button FindGlobalGoalButton() =>
            Object
                .FindObjectOfType<GlobalGoalUiView>()
                .GetComponentInChildren<Button>();

        private static Button FindNewGameButton() =>
            Object
                .FindObjectsByType<NewGameButton>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .First()
                .gameObject
                .GetComponent<Button>();
    }
}