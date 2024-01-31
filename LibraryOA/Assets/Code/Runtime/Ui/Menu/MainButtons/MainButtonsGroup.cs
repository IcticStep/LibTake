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
        [FormerlySerializedAs("_menuButtonsGroup")]
        [SerializeField]
        private MenuGroup _menuGroup;
        
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        private void Start() =>
            PlayStartAnimation()
                .Forget();

        public async UniTask Show()
        {
            _menuGroup.DisableInteractions();
            await _menuGroup.Show();
            _menuGroup.EnableInteractions();
        }

        public async UniTask Hide()
        {
            _menuGroup.DisableInteractions();
            await _menuGroup.Hide();
        }

        private async UniTask PlayStartAnimation()
        {
            _menuGroup.SetInOffScreenPosition();
            await UniTask.WaitForSeconds(_staticDataService.Ui.Menu.StartDelaySeconds);
            await Show();
        }
    }
}