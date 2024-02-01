using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Ui.Menu.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Ui.Menu.MainButtons
{
    internal sealed class MainButtonsGroup : MonoBehaviour
    {
        [SerializeField]
        private MenuGroup _menuGroup;
        
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        private void Start() =>
            PlayStartAnimation()
                .Forget();

        private async UniTask PlayStartAnimation()
        {
            _menuGroup.SetInOffScreenPosition();
            await UniTask.WaitForSeconds(_staticDataService.Ui.Menu.StartDelaySeconds);
            await _menuGroup.Show();
        }
    }
}