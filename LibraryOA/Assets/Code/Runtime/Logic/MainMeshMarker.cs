using UnityEngine;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class MainMeshMarker : MonoBehaviour
    {
        [field: SerializeField]
        public MeshRenderer Mesh { get; private set; }

        private void OnValidate() =>
            Mesh ??= GetComponent<MeshRenderer>();
    }
}