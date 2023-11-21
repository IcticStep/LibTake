using Code.Runtime.Logic.Markers.Spawns;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.Markers
{
    [CustomEditor(typeof(ReadingTableSpawn))]
    internal sealed class ReadingTableSpawnEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.Selected )]
        public static void RenderCustomGizmo(ReadingTableSpawn spawn, GizmoType gizmo)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(spawn.transform.position, 0.5f);
            Gizmos.color = previousColor;
        }
    }
}