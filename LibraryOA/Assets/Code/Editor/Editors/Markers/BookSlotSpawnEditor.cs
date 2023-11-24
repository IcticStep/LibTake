using Code.Runtime.Logic.Markers.Spawns;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.Markers
{
    [CustomEditor(typeof(BookSlotSpawn))]
    internal sealed class BookSlotSpawnEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.Selected )]
        public static void RenderCustomGizmo(BookSlotSpawn spawn, GizmoType gizmo)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawn.transform.position, 0.5f);
            Gizmos.color = previousColor;
        }
    }
}