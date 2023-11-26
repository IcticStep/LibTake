using Code.Runtime.Logic.Markers.Customers;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.Markers.Customers
{
    [CustomEditor(typeof(CustomersSpawnPoint))]
    internal sealed class CustomersSpawnPointEditor : UnityEditor.Editor
    {
        private static readonly Color _color = new(1f, 0.67f, 0.14f);

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.Selected )]
        public static void RenderCustomGizmo(CustomersSpawnPoint spawn, GizmoType gizmo)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = _color;
            Gizmos.DrawSphere(spawn.transform.position, 0.5f);
            Gizmos.color = previousColor;
        }
    }
}