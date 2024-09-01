using System;
using Code.Runtime.Infrastructure.Services.CleanUp;
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
        private readonly ILevelCleanUpService _cleanUpService;

        private DayMessage DayMessage => _hudProviderService.DayMessage;
        private MorningMessage MorningMessage => _hudProviderService.MorningMessage;
        private float MorningMessageDelay => _staticDataService.Ui.Hud.MorningMessageIntervals.OnScreenTime;
        private float DayMessageDelay => _staticDataService.Ui.Hud.DayMessageIntervals.OnScreenTime;
            
        public UiMessagesService(IHudProviderService hudProviderService, IStaticDataService staticDataService, ILevelCleanUpService cleanUpService)
        {
            _hudProviderService = hudProviderService;
            _staticDataService = staticDataService;
            _cleanUpService = cleanUpService;
        }

        public async UniTask ShowMorningMessage()
        {
            await MorningMessage.Show();
            
            try
            {
                await UniTask.WaitForSeconds(MorningMessageDelay, cancellationToken: _cleanUpService.RestartCancellationToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            
            await MorningMessage.Hide();
        }

        public async UniTask ShowDayMessage()
        {
            await DayMessage.Show();
            
            try
            {
                await UniTask.WaitForSeconds(DayMessageDelay, cancellationToken: _cleanUpService.RestartCancellationToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            
            await DayMessage.Hide();
        }
    }
}