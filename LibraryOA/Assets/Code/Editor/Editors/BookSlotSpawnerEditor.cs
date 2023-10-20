using Code.Runtime.Logic.Interactions;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors
{
    [CustomEditor(typeof(BookSlotMarker))]
    internal sealed class BookSlotSpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.Selected )]
        public static void RenderCustomGizmo(BookSlotMarker marker, GizmoType gizmo)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(marker.transform.position, 0.5f);
            Gizmos.color = previousColor;
        }
    }
}