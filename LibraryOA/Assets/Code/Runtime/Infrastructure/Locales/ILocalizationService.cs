using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.Locales
{
    internal interface ILocalizationService
    {
        UniTask WarmUp();
        void SetNextLocale();
    }
}