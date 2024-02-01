using System;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui
{
    internal sealed class Credits : MonoBehaviour
    {
        [SerializeField]
        private MoverY _moverY;
        
        private ISceneLoader _sceneLoader;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(ISceneLoader sceneLoader, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _sceneLoader = sceneLoader;
        }

        private void Start() =>
            PlayAnimation()
                .Forget();

        private async UniTaskVoid PlayAnimation()
        {
            await _moverY.MoveAsync();
            await _sceneLoader.LoadSceneAsync(_staticDataService.ScenesRouting.MenuScene);        
        }
    }
}