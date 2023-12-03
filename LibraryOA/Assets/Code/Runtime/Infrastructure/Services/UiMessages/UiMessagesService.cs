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
        private DoubleCentralMessage DoubleCentralMessage => _hudProviderService.DoubleCentralMessage;
        
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
        
        public async UniTask ShowDoubleCenterMessage(string header, string subHeader, float readingSecondsDelay = 1f)
        {
            await DoubleCentralMessage.Show(header, subHeader);
            await UniTask.WaitForSeconds(readingSecondsDelay);
            await DoubleCentralMessage.Hide();
        }
    }
}