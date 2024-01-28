using System.Collections.Generic;
using System.IO;
using System.Linq;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.StaticData.Books;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

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
            
            EditorGUILayout.LabelField("Localization updater");
            if(GUILayout.Button("Add localize string for all books miising"))
            {
                AddLocalizationForAllBooksMissing();
            }
        }

        private static void AddLocalizationForAllBooksMissing()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadBooks();
            IReadOnlyList<StaticBook> books = staticDataService.AllBooks;
            StringTableCollection collection = LocalizationEditorSettings.GetStringTableCollection("Book Localization Table");
                
            foreach(StaticBook book in books)
            {
                if(!book.LocalizedTitle.IsEmpty)
                    continue;

                foreach(StringTable stringTable in collection.StringTables)
                {
                    stringTable.AddEntry(book.Title, book.Title);
                    EditorUtility.SetDirty(stringTable);
                    EditorUtility.SetDirty(stringTable.SharedData);
                }

                book.LocalizedTitle = new LocalizedString { TableReference = "Book Localization Table", TableEntryReference = book.Title};
                EditorUtility.SetDirty(book);
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