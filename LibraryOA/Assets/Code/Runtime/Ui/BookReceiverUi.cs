using System;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Customers;
using Code.Runtime.StaticData.Books;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui
{
    internal sealed class BookReceiverUi : MonoBehaviour
    {
        [SerializeField]
        private BookReceiver _bookReceiver;
        [SerializeField]
        private SmoothFader _smoothFader;
        [SerializeField]
        private Image _bookIcon;
        [SerializeField]
        private TextMeshProUGUI _bookName;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        private void Awake() =>
            _bookReceiver.Updated += UpdateView;

        private void Start()
        {
            if(_bookReceiver.BookId is null)
                _smoothFader.FadeImmediately();
            UpdateView();
        }

        private void OnDestroy() =>
            _bookReceiver.Updated -= UpdateView;

        private void UpdateView()
        {
            SetVisibility();
            
            if(_bookReceiver.BookId is null)
                return;
            
            SetViewData();
        }

        private void SetVisibility()
        {
            if(_bookReceiver.BookId is null || _bookReceiver.Received)
                _smoothFader.Fade();
            else
                _smoothFader.UnFade();
        }

        private void SetViewData()
        {
            StaticBook data = GetBookData();
            _bookIcon.color = data.StaticBookType.UiTextColor;
            _bookName.text = data.name;
        }

        private StaticBook GetBookData() =>
            _staticDataService.ForBook(_bookReceiver.BookId);
    }
}