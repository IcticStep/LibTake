using UnityEngine;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(BookStorage))]
    internal sealed class BookStorageView : MonoBehaviour
    {
        [SerializeField] private BookStorage _bookStorage;
        [SerializeField] private GameObject _bookObject;
        
        private void Start()
        {
            _bookStorage.Updated += UpdateView;
            UpdateView();
        }

        private void OnDestroy() =>
            _bookStorage.Updated -= UpdateView;

        private void UpdateView() =>
            _bookObject.SetActive(_bookStorage.HasBook);
    }
}