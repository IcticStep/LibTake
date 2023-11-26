using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Markers.Spawns;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.Markers
{
    [CustomEditor(typeof(BookSlotSpawn))]
    internal sealed class BookSlotSpawnEditor : UnityEditor.Editor
    {
        private static readonly SpawnPreviewHelper _spawnPreviewHelper = new();
        
        private static IStaticDataService _staticDataService;
        private static Mesh _targetMesh;
        private static Vector3 _targetScale;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.Selected )]
        public static void RenderCustomGizmo(BookSlotSpawn spawn, GizmoType gizmo)
        {
            InitStaticData();
            Transform spawnTransform = spawn.transform;
            Gizmos.DrawMesh(_targetMesh, spawnTransform.position, spawnTransform.rotation, _targetScale);
        }

        private static void InitStaticData()
        {
            if(_staticDataService is not null)
                return;

            _staticDataService = new StaticDataService();
            _staticDataService.LoadInteractables();
            
            GameObject prefab = GetPrefab();
            _targetMesh = _spawnPreviewHelper.GetMesh(prefab);
            _targetScale = _spawnPreviewHelper.GetScale(prefab);
        }

        private static GameObject GetPrefab() =>
            _staticDataService
                .Interactables
                .BookSlot
                .Prefab;
    }
}