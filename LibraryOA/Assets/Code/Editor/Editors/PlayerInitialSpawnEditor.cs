using Code.Runtime.Logic.Markers;
using Code.Runtime.Logic.Markers.Spawns;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors
{
    [CustomEditor(typeof(PlayerInitialSpawn))]
    public sealed class PlayerInitialSpawnEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.Selected )]
        public static void RenderCustomGizmo(PlayerInitialSpawn spawn, GizmoType gizmo)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(spawn.transform.position, 0.5f);
            Gizmos.color = previousColor;
        }
    }
}