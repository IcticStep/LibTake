using Code.Runtime.Infrastructure.Services.HudProvider;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Ui;
using Code.Runtime.Ui.Messages;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.Services.UiMessages
{
    [UsedImplicitly]
    internal sealed class UiMessagesService : IUiMessagesService
    {
        private readonly IHudProviderService _hudProviderService;
        private readonly IStaticDataService _staticDataService;

        private DayMessage DayMessage => _hudProviderService.DayMessage;
        private MorningMessage MorningMessage => _hudProviderService.MorningMessage;
        private float MorningMessageDelay => _staticDataService.Ui.MorningMessageIntervals.OnScreenTime;
        private float DayMessageDelay => _staticDataService.Ui.DayMessageIntervals.OnScreenTime;
            
        public UiMessagesService(IHudProviderService hudProviderService, IStaticDataService staticDataService)
        {
            _hudProviderService = hudProviderService;
            _staticDataService = staticDataService;
        }

        public async UniTask ShowMorningMessage(string header, string subHeader)
        {
            await MorningMessage.Show(header, subHeader);
            await UniTask.WaitForSeconds(MorningMessageDelay);
            await MorningMessage.Hide();
        }

        public async UniTask ShowDayMessage(string text)
        {
            await DayMessage.Show(text);
            await UniTask.WaitForSeconds(DayMessageDelay);
            await DayMessage.Hide();
        }
    }
}