using Code.Runtime.Data;
using Code.Runtime.Logic.Interactions;
using UnityEngine;

namespace Code.Runtime.Logic
{
    internal sealed class BookStorageView : MonoBehaviour
    {
        [SerializeField] private BookStorageHolder _bookStorageObject;
        [SerializeField] private GameObject _bookObject;

        private IBookStorage _bookStorage;
        
        private void Start()
        {
            _bookStorage = _bookStorageObject.BookStorage;

            _bookStorage.Updated += UpdateView;
            UpdateView();
        }

        private void OnDestroy() =>
            _bookStorage.Updated -= UpdateView;

        private void UpdateView() =>
            _bookObject.SetActive(_bookStorage.HasBook);
    }
}