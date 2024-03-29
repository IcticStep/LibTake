using System.Linq;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.StaticData.Books;
using Code.Runtime.Ui.Common;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.HudComponents
{
    internal sealed class PlayerInventoryUi : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private SmoothFader _smoothFader;
        
        private IPlayerInventoryService _playerInventoryService;
        private IStaticDataService _staticDataService;
        
        [Inject]
        private void Construct(IPlayerInventoryService playerInventoryService, IStaticDataService staticDataService)
        {
            _playerInventoryService = playerInventoryService;
            _staticDataService = staticDataService;
        }

        private void Start()
        {
            _playerInventoryService.BooksUpdated += UpdateView;
            SetVisibilityImmediately();
            UpdateView();
        }
        
        private void OnDestroy() =>
            _playerInventoryService.BooksUpdated -= UpdateView;

        private void UpdateView()
        {
            SetVisibility();
            UpdateText();
        }

        private void SetVisibility()
        {
            if(_playerInventoryService.HasBook)
                _smoothFader.UnFade();
            else
                _smoothFader.Fade();
        }
        
        private void SetVisibilityImmediately()
        {
            if(_playerInventoryService.HasBook)
                _smoothFader.UnFadeImmediately();
            else
                _smoothFader.FadeImmediately();
        }

        private void UpdateText()
        {
            if(!_playerInventoryService.HasBook)
                return;

            StaticBook data = GetBookData();
            _text.text = GenerateViewTextPostfix(data);
            _text.faceColor = GetBookUiColor(data);
        }

        private string GenerateViewTextPostfix(StaticBook data)
        {
            string textResult = data.LocalizedTitle.GetLocalizedString();
            if(_playerInventoryService.BooksCount > 1)
                textResult += $" (+{_playerInventoryService.BooksCount - 1})";

            return textResult;
        }

        private Color32 GetBookUiColor(StaticBook bookData) =>
            bookData
                .StaticBookType
                .UiTextColor;

        private StaticBook GetBookData()
        {
            string bookId = _playerInventoryService.CurrentBookId;
            return _staticDataService.ForBook(bookId);
        }
    }
}