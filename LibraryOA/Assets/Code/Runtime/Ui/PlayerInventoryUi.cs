using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Player;
using Code.Runtime.StaticData.Books;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui
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
            _playerInventoryService.Updated += UpdateView;
            SetVisibilityImmediately();
            UpdateView();
        }
        
        private void OnDestroy() =>
            _playerInventoryService.Updated -= UpdateView;

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
            
            _text.text = GenerateViewText();
        }

        private string GenerateViewText()
        {
            StaticBook bookData = GetBookData();
            string textResult = bookData.Title;
            if(_playerInventoryService.Count > 1)
                textResult += $"(+{_playerInventoryService.Count - 1})";

            return textResult;
        }

        private StaticBook GetBookData()
        {
            string bookId = _playerInventoryService.CurrentBookId;
            StaticBook bookData = _staticDataService.ForBook(bookId);
            return bookData;
        }
    }
}