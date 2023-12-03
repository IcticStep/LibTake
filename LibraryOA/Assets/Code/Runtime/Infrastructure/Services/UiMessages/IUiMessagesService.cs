using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.Services.UiMessages
{
    internal interface IUiMessagesService
    {
        UniTask ShowCenterMessage(string text, float readingSecondsDelay = 1f);
    }
}