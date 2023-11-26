using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Editor.Windows.InteractablesEditorSpawn
{
    public class InteractablesSpawn : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset _visualTreeAsset;
        private Label _infoBoxText;
        private Button _spawnButton;
        private Button _despawnButton;

        [MenuItem("Tools/Interactables Preview Spawn")]
        public static void ShowExample()
        {
            InteractablesSpawn window = GetWindow<InteractablesSpawn>();
            window.titleContent = new GUIContent("InteractablesSpawn");
        }

        private void CreateGUI()
        {
            VisualElement content = _visualTreeAsset.Instantiate();
            rootVisualElement.Add(content);
            _infoBoxText = rootVisualElement.Q<Label>("InfoBoxText");
            _spawnButton = rootVisualElement.Q<Button>("SpawnInteractablesButton");
            _despawnButton = rootVisualElement.Q<Button>("DespawnInteractablesButton");

            _spawnButton.clicked += SpawnInteractableObjectsPreview;
            
            UpdateInfoBoxText();
        }

        private void UpdateInfoBoxText() =>
            _infoBoxText.text = $"Currently spawned in preview: {0} interactables";

        private void SpawnInteractableObjectsPreview()
        {
            UpdateInfoBoxText();
        }
        
        private void DeleteInteractableObjectsPreview()
        {
            UpdateInfoBoxText();
        }
    }
}
