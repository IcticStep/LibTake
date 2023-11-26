using Code.Runtime.Logic;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.Logic
{
    [CustomEditor(typeof(Library))]
    public class LibraryEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Library library = (Library)target;
            if(GUILayout.Button("Turn on 2nd floor objects"))
                library.TurnOnSecondFloor();
            if(GUILayout.Button("Turn off 2nd floor objects"))
                library.TurnOffSecondFloor();
        }
    }
}