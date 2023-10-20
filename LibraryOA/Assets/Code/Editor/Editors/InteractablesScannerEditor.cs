using Code.Runtime.Player;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors
{
    [CustomEditor(typeof(InteractablesScanner))]
    public sealed class InteractablesScannerEditor : UnityEditor.Editor
    {
        private const int RaycastGizmoThickness = 5;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderRaycastGizmo(InteractablesScanner scanner, GizmoType gizmo)
        {
            if(scanner.RayStart is null) return;
            
            Color previousColor = Handles.color;
            Handles.color = Color.green;
            
            Vector3 start = scanner.RayStart.Value;
            Vector3 end = start + scanner.gameObject.transform.forward * scanner.RayLength;
            
            Handles.DrawLine(start, end, RaycastGizmoThickness);
            Handles.color = previousColor;
        }
    }
}