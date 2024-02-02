using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Loading;
using Code.Runtime.Ui.Menu.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui.Menu.MainButtons
{
    internal sealed class Authors : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private MenuGroup _mainButtonsGroup;
        [SerializeField]
        private MenuGroup _nameGroup;
        
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

        private void Awake() =>
            _button.onClick.AddListener(OnContinueButtonPressed);
        
        private void OnDestroy() =>
            _button.onClick.RemoveListener(OnContinueButtonPressed);
        
        private void OnContinueButtonPressed() =>
            ShowSettings()
                .Forget();

        private async UniTaskVoid ShowSettings()
        {
            await UniTask.WhenAll(
                _mainButtonsGroup.Hide(),
                _nameGroup.Hide(),
                _loadingCurtainService.ShowBlackAsync());
            await _sceneLoader.LoadSceneAsync(_staticDataService.ScenesRouting.AuthorsScene);
        }
    }
}