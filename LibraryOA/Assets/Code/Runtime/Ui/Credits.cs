using System;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Loading;
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
        private ILoadingCurtainService _loadingCurtainService;

        [Inject]
        private void Construct(ISceneLoader sceneLoader, IStaticDataService staticDataService, ILoadingCurtainService loadingCurtainService)
        {
            _loadingCurtainService = loadingCurtainService;
            _staticDataService = staticDataService;
            _sceneLoader = sceneLoader;
        }

        private void Start() =>
            PlayAnimation()
                .Forget();

        private async UniTaskVoid PlayAnimation()
        {
            _loadingCurtainService.HideBlackImmediately();
            await _moverY.MoveAsync();
            _loadingCurtainService.ShowBlackImmediately();
            await _sceneLoader.LoadSceneAsync(_staticDataService.ScenesRouting.MenuScene);     
        }
    }
}