using System.Collections;
using Code.PlayModeTests.Navigation;
using Cysharp.Threading.Tasks;
using UnityEngine.TestTools;
using static Code.PlayModeTests.Navigation.MainMenuNavigator;

namespace Code.PlayModeTests
{
    public class PlayModeExceptionsTests
    {
        [UnityTest]
        public IEnumerator OpenMainMenu_Wait() =>
            UniTask.ToCoroutine(
                async () =>
                {
                    await OpenMainMenu();
                });

        [UnityTest]
        public IEnumerator OpenMainMenu_StartGame() =>
            UniTask.ToCoroutine(
                async () =>
                {
                    await OpenMainMenu();
                    await StartNewGame();
                    await StartFirstGlobalGoal();
                    await UniTask.WaitForSeconds(5f);
                });
        
        [UnityTest]
        public IEnumerator OpenMainMenu_StartGame_ReturnToMainMenu() =>
            UniTask.ToCoroutine(
                async () =>
                {
                    await OpenMainMenu();
                    await StartNewGame();
                    await StartFirstGlobalGoal();
                    await LevelNavigator.ExitToMainMenu();
                    await UniTask.WaitForSeconds(1f);
                });
    }
}
