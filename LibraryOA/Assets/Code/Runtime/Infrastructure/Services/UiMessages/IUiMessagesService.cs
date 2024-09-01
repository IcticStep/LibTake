using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.Services.UiMessages
{
    internal interface IUiMessagesService
    {
        UniTask ShowMorningMessage();
        UniTask ShowDayMessage();
    }
}