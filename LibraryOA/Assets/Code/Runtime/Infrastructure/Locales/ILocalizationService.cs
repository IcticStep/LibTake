using System;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.Locales
{
    internal interface ILocalizationService : IDisposable
    {
        UniTask WarmUp();
        void SetNextLocale();
        event Action LocaleChanged;
    }
}