using UnityEngine;

namespace Code.Runtime.Logic
{
    public class Book : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _meshRenderer;

        public void SetMaterial(Material material) =>
            _meshRenderer.material = material;
    }
}