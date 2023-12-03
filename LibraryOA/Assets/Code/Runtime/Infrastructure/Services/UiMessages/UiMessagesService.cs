using Code.Runtime.Infrastructure.Services.HudProvider;
using Code.Runtime.Ui;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.Services.UiMessages
{
    [UsedImplicitly]
    internal sealed class UiMessagesService : IUiMessagesService
    {
        private readonly IHudProviderService _hudProviderService;

        private CentralMessage CentralMessage => _hudProviderService.CentralMessage;
        
        public UiMessagesService(IHudProviderService hudProviderService) 
        {
            _hudProviderService = hudProviderService;
        }

        public async UniTask ShowCenterMessage(string text, float readingSecondsDelay = 1f)
        {
            await CentralMessage.Show(text);
            await UniTask.WaitForSeconds(readingSecondsDelay);
            await CentralMessage.Hide();
        }
    }
}