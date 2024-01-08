using Code.Runtime.Logic.Interactables;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.Logic
{
    [CustomEditor(typeof(BookStorage))]
    public class BookStorageEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            BookStorage bookStorage = (BookStorage)target;
            base.OnInspectorGUI();

            string bookView = bookStorage.HasBook ? bookStorage.CurrentBookId : "none";
            GUILayout.Label($"Current book: {bookView}");
        }
    }
}