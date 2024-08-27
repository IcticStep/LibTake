using System;
using System.Collections;
using System.Linq;
using Code.PlayModeTests.Extensions;
using Code.Runtime.Ui.Menu.GlobalGoals;
using Code.Runtime.Ui.Menu.MainButtons;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Code.PlayModeTests
{
    public class PlayModeExceptionsTests
    {
        // To do: Refactor tests to use more high-level abstractions
        
        [UnityTest]
        public IEnumerator OpenMainMenu() =>
            UniTask.ToCoroutine(
                async () =>
                {
                    await SceneManager.LoadSceneAsync(0);
                    await UniTask.WaitForSeconds(1f);
                });
        
        [UnityTest]
        public IEnumerator OpenMainMenu_StartGame() =>
            UniTask.ToCoroutine(
                async () =>
                {
                    await SceneManager.LoadSceneAsync(0);
                    await UniTask.WaitForSeconds(2f);

                    FindNewGameButton().SimulateClick();
                    await UniTask.WaitForSeconds(2f);

                    FindGlobalGoalButton().SimulateClick();
                    await UniTask.WaitForSeconds(5f);
                });

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
