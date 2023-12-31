using Code.Runtime.Logic.Markers.Truck;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.Markers
{
    public static class TruckWayGizmoDrawer
    {
        [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
        static void DrawGizmosForTruckWay(TruckWay truckWay, GizmoType gizmoType)
        {
            if(truckWay.LibraryPoint == null || truckWay.HiddenPoint == null)
                return;

            Handles.color = Color.blue;
            Handles.DrawLine(truckWay.LibraryPoint.transform.position, truckWay.HiddenPoint.transform.position, 2f);
        }
    }
}