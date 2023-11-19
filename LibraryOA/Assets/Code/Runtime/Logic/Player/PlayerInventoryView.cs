using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Player;
using Code.Runtime.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Player
{
    internal sealed class PlayerInventoryView : MonoBehaviour
    {
        [SerializeField] private GameObject _bookObject;
        private IPlayerInventoryService _playerInventoryService;
        private MeshRenderer _bookMeshRenderer;
        private IStaticDataService _staticData;

        [Inject]
        private void Construct(IPlayerInventoryService playerInventoryService, IStaticDataService staticDataService)
        {
            _playerInventoryService = playerInventoryService;
            _staticData = staticDataService;
        }

        private void Awake() =>
            _playerInventoryService.Updated += UpdateView;

        private void Start()
        {
            _bookMeshRenderer = _bookObject.GetComponent<MeshRenderer>();
            UpdateView();
        }

        private void OnDestroy() =>
            _playerInventoryService.Updated -= UpdateView;

        private void UpdateView()
        {
            SetMaterialIfAny();
            _bookObject.SetActive(_playerInventoryService.HasBook);
        }

        private void SetMaterialIfAny()
        {
            Material targetMaterial = GetBookMaterial();
            if(targetMaterial is null)
                return;

            _bookMeshRenderer.material = targetMaterial;
        }

        private Material GetBookMaterial()
        {
            string bookId = _playerInventoryService.CurrentBookId;
            if(string.IsNullOrWhiteSpace(bookId))
                return null;
            
            StaticBook data = _staticData.ForBook(bookId);
            return data.StaticBookType.Material;
        }
    }
}