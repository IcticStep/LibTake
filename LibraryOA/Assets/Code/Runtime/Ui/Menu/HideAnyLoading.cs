using Code.Runtime.Services.Loading;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.Menu
{
    internal sealed class HideAnyLoading : MonoBehaviour
    {
        private ILoadingCurtainService _loadingCurtainService;

        [Inject]
        private void Construct(ILoadingCurtainService loadingCurtainService) =>
            _loadingCurtainService = loadingCurtainService;

        private void Start()
        {
            if(_loadingCurtainService.ImageVisible)
            {
                _loadingCurtainService.HideImageAsync();
                return;
            }
            
            if(_loadingCurtainService.BlackVisible)
                _loadingCurtainService.HideBlackAsync();
        }
    }
}