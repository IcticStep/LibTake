using Code.Editor.Windows.BookSlot;
using Code.Runtime.Logic.Markers.Spawns;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.Markers
{
    [CustomEditor(typeof(BookSlotSpawnContainer))]
    internal sealed class BookSlotSpawnContainerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.Selected)]
        public static void RenderCustomGizmo(BookSlotSpawnContainer container, GizmoType gizmo)
        {
            if(!EditorWindow.HasOpenInstances<BookSlotSpawnsPlacer>())
                return;
            
            Color previousColor = Handles.color;
            Handles.color = Color.red;
            Handles.DrawWireDisc(container.transform.position, Vector3.up, container.CircleRadius, 1f);
            Handles.color = previousColor;
        }
    }
}