using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Ui.Menu.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.Menu.MainButtons
{
    internal sealed class MainButtonsGroup : MonoBehaviour
    {
        [SerializeField]
        private MenuButtonsGroup _menuButtonsGroup;
        
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        private void Start() =>
            PlayStartAnimation()
                .Forget();

        public async UniTask Show()
        {
            _menuButtonsGroup.DisableInteractions();
            await _menuButtonsGroup.Show();
            _menuButtonsGroup.EnableInteractions();
        }

        public async UniTask Hide()
        {
            _menuButtonsGroup.DisableInteractions();
            await _menuButtonsGroup.Hide();
        }

        private async UniTask PlayStartAnimation()
        {
            _menuButtonsGroup.SetInOffScreenPosition();
            await UniTask.WaitForSeconds(_staticDataService.Ui.Menu.StartDelaySeconds);
            await Show();
        }
    }
}