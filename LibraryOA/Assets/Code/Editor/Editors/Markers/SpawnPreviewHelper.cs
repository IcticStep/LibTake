using Code.Runtime.Logic;
using UnityEngine;

namespace Code.Editor.Editors.Markers
{
    internal sealed class SpawnPreviewHelper
    {
        public Mesh GetMesh(GameObject prefab) =>
            prefab
                .GetComponentInChildren<MainMeshGetter>()
                .Mesh;

        public Vector3 GetScale(GameObject prefab) =>
            prefab
                .transform
                .lossyScale;
    }
}