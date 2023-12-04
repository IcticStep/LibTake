using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Interactions;
using Code.Runtime.StaticData.Books;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    internal sealed class BookStorageView : MonoBehaviour
    {
        [SerializeField] 
        private BookStorage _bookStorage;
        [SerializeField]
        private Book _bookObject;
        [SerializeField]
        private MeshRenderer _bookMeshRenderer;
        
        private IStaticDataService _staticData;

        [Inject]
        private void Construct(IStaticDataService staticData) =>
            _staticData = staticData;

        private void Start()
        {
            _bookStorage.BooksUpdated += UpdateView;
            UpdateView();
        }

        private void OnDestroy() =>
            _bookStorage.BooksUpdated -= UpdateView;

        private void UpdateView()
        {
            SetMaterialIfAny();
            _bookMeshRenderer.enabled = _bookStorage.HasBook;
        }

        private void SetMaterialIfAny()
        {
            Material targetMaterial = GetBookMaterial();
            if(targetMaterial is null)
                return;

            _bookObject.SetMaterial(targetMaterial);
        }

        private Material GetBookMaterial()
        {
            string bookId = _bookStorage.CurrentBookId;
            if(string.IsNullOrWhiteSpace(bookId))
                return null;
            
            StaticBook data = _staticData.ForBook(bookId);
            return data.StaticBookType.Material;
        }
    }
}