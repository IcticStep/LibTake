using Code.Runtime.Logic.Interactions;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors
{
    [CustomEditor(typeof(BookSlotSpawnMarker))]
    internal sealed class BookSlotSpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.Selected )]
        public static void RenderCustomGizmo(BookSlotSpawnMarker spawnMarker, GizmoType gizmo)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawnMarker.transform.position, 0.5f);
            Gizmos.color = previousColor;
        }
    }
}