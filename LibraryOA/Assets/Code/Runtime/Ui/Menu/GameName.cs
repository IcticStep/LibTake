using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.Menu
{
    internal sealed class GameName : MonoBehaviour
    {
        [SerializeField]
        private FastFlyingFading _fader;

        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        private void Start() =>
            PlayStartAnimation().Forget();

        public UniTask Show() =>
            _fader.FadeIn(this.GetCancellationTokenOnDestroy());

        public UniTask Hide() =>
            _fader.FadeOut(this.GetCancellationTokenOnDestroy());

        private async UniTaskVoid PlayStartAnimation()
        {
            _fader.SetInOffScreenPosition();
            await UniTask.WaitForSeconds(_staticDataService.Ui.Menu.StartDelaySeconds);
            await Show();
        }
    }
}