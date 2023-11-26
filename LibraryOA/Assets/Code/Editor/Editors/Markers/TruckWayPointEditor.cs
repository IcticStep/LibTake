using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Markers.Spawns;
using Code.Runtime.Logic.Markers.Truck;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.Markers
{
    [CustomEditor(typeof(TruckWayPoint))]
    internal sealed class TruckWayPointEditor : UnityEditor.Editor
    {
        private static readonly SpawnPreviewHelper _spawnPreviewHelper = new();
        private static readonly Vector3 _offset = Vector3.up * 2f;

        private static IStaticDataService _staticDataService;
        private static Mesh _targetMesh;
        private static Vector3 _targetScale;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.Selected )]
        public static void RenderCustomGizmo(TruckWayPoint point, GizmoType gizmo)
        {
            InitStaticData();
            Transform spawnTransform = point.transform;
            Gizmos.DrawMesh(_targetMesh, spawnTransform.position + _offset, spawnTransform.rotation, _targetScale);
        }
        
        private static void InitStaticData()
        {
            if(_staticDataService is not null)
                return;

            _staticDataService = new StaticDataService();
            _staticDataService.LoadInteractables();
            
            GameObject bookSlotPrefab = GetPrefab();
            _targetMesh = _spawnPreviewHelper.GetMesh(bookSlotPrefab);
            _targetScale = _spawnPreviewHelper.GetScale(bookSlotPrefab);
        }

        private static GameObject GetPrefab() =>
            _staticDataService
                .Interactables
                .Truck
                .Prefab;
    }
}