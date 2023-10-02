using System;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.Services
{
    internal interface ISceneLoader
    {
        public UniTask LoadSceneAsync(string sceneName, Action onLoaded = null);
    }
}