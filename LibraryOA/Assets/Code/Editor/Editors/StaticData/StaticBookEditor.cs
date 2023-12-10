using Code.Runtime.StaticData.Books;
using UnityEditor;

namespace Code.Editor.Editors.StaticData
{
    [CustomEditor(typeof(StaticBook))]
    public class StaticBookEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}