using System.Collections.Generic;
using Code.Runtime.Data;
using Code.Runtime.Logic.Player;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.Logic
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
            
            Vector3 position = scanner.RayStart.Value;

            IReadOnlyList<Line> rays = GetRays(scanner);
            Handles.color = Color.green;

            foreach(Line line in rays)
                Handles.DrawLine(line.Start, line.End, RaycastGizmoThickness);
            
            Handles.color = previousColor;
        }

        private static IReadOnlyList<Line> GetRays(InteractablesScanner scanner) =>
            scanner.CurrentRays 
            ?? scanner
                .CreateRayVectorsRotator()
                .CreateVectorsRotated(scanner.RayStart!.Value, scanner.StartPointForward);
    }
}