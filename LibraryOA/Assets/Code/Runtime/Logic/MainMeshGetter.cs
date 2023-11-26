using UnityEngine;

namespace Code.Runtime.Logic
{
    public sealed class MainMeshGetter : MonoBehaviour
    {
        [field: SerializeField]
        private MeshFilter _meshFilter;
        [field: SerializeField]
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        
        public Mesh Mesh
        {
            get
            {
                if(_meshFilter != null)
                    return _meshFilter.sharedMesh;

                if(_skinnedMeshRenderer != null)
                    return _skinnedMeshRenderer.sharedMesh;

                Debug.LogError($"Can't find mesh on object {gameObject.name}!", this);
                return null;
            }
        }

        private void OnValidate()
        {
            if(TryGetComponent(out MeshFilter meshFilter))
                _meshFilter ??= meshFilter;
            if(TryGetComponent(out SkinnedMeshRenderer skinnedMeshRenderer))
                _skinnedMeshRenderer ??= skinnedMeshRenderer;
        }
    }
}