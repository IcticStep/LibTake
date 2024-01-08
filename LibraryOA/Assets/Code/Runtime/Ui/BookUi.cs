using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Interactables;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.Services.Interactions.Scanning;
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
        private GameObject _readBookIcon;
        [SerializeField]
        private GameObject _scannedBookIcon;
        [SerializeField]
        private BookStorage _bookStorage;
        [SerializeField]
        private SmoothFader _smoothFader;
        
        private IStaticDataService _staticDataService;
        private IReadBookService _readBookService;
        private IScanBookService _scanBookService;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IReadBookService readBookService, IScanBookService scanBookService)
        {
            _scanBookService = scanBookService;
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
            
            SetBookName(bookData);
            SetReadStatus(bookId);
            SetScannedStatus(bookId);
            
            _smoothFader.UnFade();
        }

        public void HideData()
        {
            if(_smoothFader.IsFullyInvisible)
                return;
                
            _smoothFader.Fade();
        }

        private void SetReadStatus(string bookId)
        {
            bool isRead = _readBookService.IsRead(bookId);
            _readBookIcon.SetActive(isRead);
        }

        private void SetScannedStatus(string bookId)
        {
            bool isScanned = _scanBookService.IsScanned(bookId);
            _scannedBookIcon.SetActive(isScanned);
        }

        private void SetBookName(StaticBook bookData) =>
            _bookName.text = bookData.name;
    }
}