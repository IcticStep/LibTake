using System;
using System.Collections;
using System.Linq;
using Code.PlayModeTests.Extensions;
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
        [UnityTest]
        public IEnumerator WhenNavigatedToMainMenu_ThenNoExceptionsThrown() =>
            UniTask.ToCoroutine(
                async () =>
                {
                    await SceneManager.LoadSceneAsync(0);
                    await UniTask.WaitForSeconds(1f);
                });
        
        [UnityTest]
        public IEnumerator WhenStartedGame_AndWaited_ThenNoExceptionsThrown() =>
            UniTask.ToCoroutine(
                async () =>
                {
                    await SceneManager.LoadSceneAsync(0);
                    await UniTask.WaitForSeconds(1f);

                    FindNewGameButton().SimulateClick();
                    await UniTask.WaitForSeconds(1f); 
                });

        private static Button FindNewGameButton() =>
            Object
                .FindObjectsByType<NewGameButton>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .First()
                .gameObject
                .GetComponent<Button>();
    }
}
