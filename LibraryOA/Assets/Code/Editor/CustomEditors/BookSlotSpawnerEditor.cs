using Code.Runtime.Logic.Interactions;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.CustomEditors
{
    [CustomEditor(typeof(BookSlotSpawner))]
    internal sealed class BookSlotSpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(BookSlotSpawner spawner, GizmoType gizmo)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawner.transform.position, 0.5f);
            Gizmos.color = previousColor;
        }
    }
}