using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Interactables;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.StaticData.Books;
using Code.Runtime.Ui.Common;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui
{
    internal sealed class BookUi : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _bookName;
        [SerializeField]
        private GameObject _readTick;
        [SerializeField]
        private BookStorage _bookStorage;
        [SerializeField]
        private SmoothFader _smoothFader;
        
        private IStaticDataService _staticDataService;
        private IReadBookService _readBookService;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IReadBookService readBookService)
        {
            _readBookService = readBookService;
            _staticDataService = staticDataService;
        }

        private void Start() =>
            _smoothFader.FadeImmediately();

        public void ShowData()
        {
            if(!_bookStorage.HasBook)
                return;
            
            string bookId = _bookStorage.CurrentBookId;
            StaticBook bookData = _staticDataService.ForBook(bookId);
            bool isRead = _readBookService.IsRead(bookId);
            _bookName.text = bookData.name;
            
            _readTick.SetActive(isRead);
            _smoothFader.UnFade();
        }

        public void HideData()
        {
            if(_smoothFader.IsFullyInvisible)
                return;
                
            _smoothFader.Fade();
        }
    }
}