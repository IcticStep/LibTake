using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Code.PlayModeTests
{
    public class PlayModeExceptionsTests
    {
        [UnityTest]
        public IEnumerator WhenNavigatedToMainMenu_ThenNoExceptionsThrown() =>
            UniTask.ToCoroutine(
                async () =>
                {
                    // Arrange.
                    await SceneManager.LoadSceneAsync(0);

                    // Act.
                    await UniTask.WaitForSeconds(1f);
                });
    }
}
