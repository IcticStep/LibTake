using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Code.Runtime.Infrastructure.Services.Locales
{
    [UsedImplicitly]
    internal sealed class LocalizationService : ILocalizationService
    {
        private readonly List<Locale> _availableLocalizations = new();
        private int _selectedLocaleIndex;

        public event Action LocaleChanged;
        
        public async UniTask WarmUp()
        {
            await LocalizationSettings.InitializationOperation;

            _availableLocalizations.AddRange(LocalizationSettings.AvailableLocales.Locales);
            _selectedLocaleIndex = _availableLocalizations.FindIndex(locale => locale == LocalizationSettings.SelectedLocale);
            LocalizationSettings.SelectedLocaleChanged += NotifyLocalizationChanged;
        }

        public void Dispose() =>
            LocalizationSettings.SelectedLocaleChanged -= NotifyLocalizationChanged;

        public void SetNextLocale()
        {
            _selectedLocaleIndex = (_selectedLocaleIndex + 1) % _availableLocalizations.Count;
            LocalizationSettings.SelectedLocale = _availableLocalizations[_selectedLocaleIndex];
        }

        private void NotifyLocalizationChanged(Locale locale) =>
            LocaleChanged?.Invoke();
    }
}