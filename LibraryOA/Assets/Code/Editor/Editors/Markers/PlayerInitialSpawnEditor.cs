using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Markers.Spawns;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.Markers
{
    [CustomEditor(typeof(PlayerInitialSpawn))]
    public sealed class PlayerInitialSpawnEditor : UnityEditor.Editor
    {
        private const float OffsetMultiplier = 0.01f;
        private static readonly SpawnPreviewHelper _spawnPreviewHelper = new();
        private static readonly Quaternion _rotationOffset = Quaternion.Euler(90, 0, 0);

        private static IStaticDataService _staticDataService;
        private static Mesh _targetMesh;
        private static Vector3 _targetScale;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.Selected )]
        public static void RenderCustomGizmo(PlayerInitialSpawn spawn, GizmoType gizmo)
        {
            InitStaticData();
            Transform spawnTransform = spawn.transform;
            Quaternion rotation = spawnTransform.rotation * _rotationOffset;
            Gizmos.DrawMesh(_targetMesh, spawnTransform.position, rotation, _targetScale);
        }
        
        private static void InitStaticData()
        {
            if(_staticDataService is not null)
                return;

            _staticDataService = new StaticDataService();
            _staticDataService.LoadPlayer();
            
            GameObject prefab = GetPrefab();
            _targetMesh = _spawnPreviewHelper.GetMesh(prefab);
            _targetScale = _spawnPreviewHelper.GetScale(prefab) * OffsetMultiplier;
        }

        private static GameObject GetPrefab() =>
            _staticDataService
                .Player
                .Prefab;
    }
}