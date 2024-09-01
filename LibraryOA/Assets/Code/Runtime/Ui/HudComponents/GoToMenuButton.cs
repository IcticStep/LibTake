using System;
using Code.Runtime.Infrastructure.Services.CleanUp;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Loading;
using Code.Runtime.StaticData.Interactables;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui.HudComponents
{
    internal sealed class GoToMenuButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        
        private ILoadingCurtainService _loadingCurtainService;
        private ISceneLoader _sceneLoader;
        private IStaticDataService _staticDataService;
        private ILevelCleanUpService _levelCleanUpService;

        [Inject]
        private void Construct(ILoadingCurtainService loadingCurtainService, ISceneLoader sceneLoader, IStaticDataService staticDataService,
            ILevelCleanUpService levelCleanUpService)
        {
            _levelCleanUpService = levelCleanUpService;
            _staticDataService = staticDataService;
            _sceneLoader = sceneLoader;
            _loadingCurtainService = loadingCurtainService;
        }

        public void GoToMenu()
        {
            _button.interactable = false;
            GoToMenuAsync().Forget();
        }

        private async UniTaskVoid GoToMenuAsync()
        {
            await _loadingCurtainService.ShowBlackAsync();
            _levelCleanUpService.CleanUp();
            await _sceneLoader.LoadSceneAsync(_staticDataService.ScenesRouting.MenuScene);
        }
    }
}