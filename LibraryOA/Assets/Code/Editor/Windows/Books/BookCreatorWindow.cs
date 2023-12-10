using System.IO;
using Code.Runtime.StaticData.Books;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Windows.Books
{
    public class BookCreatorWindow : EditorWindow
    {
        private string bookTitle = "";
        private StaticBookType selectedBookType;

        [MenuItem("Tools/Book Creator")]
        public static void ShowWindow()
        {
            GetWindow<BookCreatorWindow>("Book Creator");
        }

        private void OnGUI()
        {
            GUILayout.Label("Create a New Book", EditorStyles.boldLabel);

            bookTitle = EditorGUILayout.TextField("Title", bookTitle);

            // Object field for selecting StaticBookType asset
            selectedBookType = EditorGUILayout.ObjectField("Book Type", selectedBookType, typeof(StaticBookType), false) as StaticBookType;

            if (GUILayout.Button("Create Book"))
            {
                CreateBook();
            }
        }

        private void CreateBook()
        {
            StaticBook newBook = CreateInstance<StaticBook>();
            newBook.Title = bookTitle;
            newBook.StaticBookType = selectedBookType;
            
            string folderPath = Path.Combine("Assets/Resources/Static Data/Books/Instances", selectedBookType != null ? selectedBookType.name : "Other");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string assetPath = Path.Combine(folderPath, bookTitle + ".asset");
            AssetDatabase.CreateAsset(newBook, assetPath);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = newBook;
        }
    }
}