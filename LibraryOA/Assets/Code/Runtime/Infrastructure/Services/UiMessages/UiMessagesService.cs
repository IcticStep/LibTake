using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.Services.UiHud;
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
        private float MorningMessageDelay => _staticDataService.Ui.Hud.MorningMessageIntervals.OnScreenTime;
        private float DayMessageDelay => _staticDataService.Ui.Hud.DayMessageIntervals.OnScreenTime;
            
        public UiMessagesService(IHudProviderService hudProviderService, IStaticDataService staticDataService)
        {
            _hudProviderService = hudProviderService;
            _staticDataService = staticDataService;
        }

        public async UniTask ShowMorningMessage()
        {
            await MorningMessage.Show();
            await UniTask.WaitForSeconds(MorningMessageDelay);
            await MorningMessage.Hide();
        }

        public async UniTask ShowDayMessage()
        {
            await DayMessage.Show();
            await UniTask.WaitForSeconds(DayMessageDelay);
            await DayMessage.Hide();
        }
    }
}