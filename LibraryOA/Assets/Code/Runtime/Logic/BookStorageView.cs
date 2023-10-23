using Code.Runtime.Data;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Interactions;
using Code.Runtime.Logic.Interactions.Data;
using Code.Runtime.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    internal sealed class BookStorageView : MonoBehaviour
    {
        [SerializeField] private BookStorageHolder _bookStorageObject;
        [SerializeField] private GameObject _bookObject;

        private IBookStorage _bookStorage;
        private MeshRenderer _bookMeshRenderer;
        private IStaticDataService _staticData;

        [Inject]
        private void Construct(IStaticDataService staticData) =>
            _staticData = staticData;

        private void Start()
        {
            _bookStorage = _bookStorageObject.BookStorage;
            _bookMeshRenderer = _bookObject.GetComponent<MeshRenderer>();

            _bookStorage.Updated += UpdateView;
            UpdateView();
        }

        private void OnDestroy() =>
            _bookStorage.Updated -= UpdateView;

        private void UpdateView()
        {
            SetMaterialIfAny();
            _bookObject.SetActive(_bookStorage.HasBook);
        }

        private void SetMaterialIfAny()
        {
            Material material = GetBookMaterial();
            if(material is not null)
                _bookMeshRenderer.material = material;
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