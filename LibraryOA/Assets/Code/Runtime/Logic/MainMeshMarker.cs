using UnityEngine;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class MainMeshMarker : MonoBehaviour
    {
        [field: SerializeField]
        public MeshFilter MeshFilter { get; private set; }

        private void OnValidate() =>
            MeshFilter ??= GetComponent<MeshFilter>();
    }
}