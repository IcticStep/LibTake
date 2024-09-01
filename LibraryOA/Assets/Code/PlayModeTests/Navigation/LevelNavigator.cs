using System;
using System.Linq;
using System.Threading.Tasks;
using Code.PlayModeTests.Extensions;
using Code.Runtime.Ui.HudComponents;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Code.PlayModeTests.Navigation
{
    internal static class LevelNavigator
    {
        public static async UniTask OpenPauseMenu()
        {
            FindPauseButton().SimulateClick();
            await UniTask.WaitForSeconds(5f);
        }

        public static async UniTask ExitToMainMenu()
        {
            FindToMainMenuButton().SimulateClick();
            await UniTask.WaitForSeconds(2f, ignoreTimeScale: true);
        }

        private static Button FindToMainMenuButton() =>
            Object
                .FindObjectOfType<GoToMenuButton>()
                .GetComponent<Button>();

        private static Button FindPauseButton() =>
            Object
                .FindObjectsByType<Button>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .First(button => button.name.Contains("Pause", StringComparison.InvariantCultureIgnoreCase))
                .gameObject
                .GetComponent<Button>();
    }
}