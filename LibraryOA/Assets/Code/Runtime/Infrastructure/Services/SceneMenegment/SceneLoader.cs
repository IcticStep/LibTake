using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace Code.Runtime.Infrastructure.Services.SceneMenegment
{
    [UsedImplicitly]
    internal sealed class SceneLoader : ISceneLoader
    {
        public async UniTask LoadSceneAsync(string sceneName, Action onLoaded = null)
        {
            bool alreadyOnScene = SceneManager.GetActiveScene().name == sceneName;
            if (alreadyOnScene)
            {
                onLoaded?.Invoke();
                return;
            }

            await SceneManager.LoadSceneAsync(sceneName);
            onLoaded?.Invoke();
        }
    }
}