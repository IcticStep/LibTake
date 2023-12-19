using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.Services.UiMessages
{
    internal interface IUiMessagesService
    {
        UniTask ShowDayMessage(string text);
        UniTask ShowMorningMessage(string header, string subHeader);
    }
}