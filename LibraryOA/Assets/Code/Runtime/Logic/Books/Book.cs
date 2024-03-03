using UnityEngine;

namespace Code.Runtime.Logic.Books
{
    public class Book : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _meshRenderer;

        public void SetMaterial(Material material) =>
            _meshRenderer.sharedMaterial = material;

        public void Show() =>
            _meshRenderer.enabled = true;
        
        public void Hide() =>
            _meshRenderer.enabled = false;
    }
}