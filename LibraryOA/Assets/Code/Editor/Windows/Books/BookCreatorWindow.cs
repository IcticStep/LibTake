using System.IO;
using Code.Runtime.StaticData.Books;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Windows.Books
{
    public class BookCreatorWindow : EditorWindow
    {
        private string _bookTitle = "";
        private StaticBookType _selectedBookType;

        [MenuItem("Tools/Book Creator")]
        public static void ShowWindow() =>
            GetWindow<BookCreatorWindow>("Book Creator");

        private void OnGUI()
        {
            GUILayout.Label("Create a New Book", EditorStyles.boldLabel);

            _bookTitle = DrawBookTitleField();
            _selectedBookType = DrawBookTypeField();

            if (GUILayout.Button("Create Book"))
            {
                CreateBook();
                CleanUpTool();
            }
        }

        private string DrawBookTitleField() =>
            EditorGUILayout.TextField("Title", _bookTitle);

        private StaticBookType DrawBookTypeField() =>
            EditorGUILayout.ObjectField("Book Type", _selectedBookType, typeof(StaticBookType), false) as StaticBookType;

        private void CreateBook()
        {
            StaticBook newBook = CreateInstance<StaticBook>();
            newBook.Title = _bookTitle;
            newBook.StaticBookType = _selectedBookType;
    
            string folderPath = GetDirectoryPath();
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string assetPath = GetBookPath(folderPath);
            AssetDatabase.CreateAsset(newBook, assetPath);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = newBook;
        }

        private void CleanUpTool()
        {
            _bookTitle = "";
            Repaint();
        }

        private string GetDirectoryPath() =>
            Path.Combine("Assets/Resources/Static Data/Books/Instances", _selectedBookType != null ? _selectedBookType.name : "Other");

        private string GetBookPath(string folderPath) =>
            Path.Combine(folderPath, _bookTitle + ".asset");
    }
}