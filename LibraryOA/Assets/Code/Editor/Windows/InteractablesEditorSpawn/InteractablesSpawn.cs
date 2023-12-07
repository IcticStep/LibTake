using System.Collections.Generic;
using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Markers;
using Code.Runtime.Services.Customers.Registry;
using Code.Runtime.Services.Interactions.Registry;
using Code.Runtime.Services.TruckDriving;
using Code.Runtime.StaticData.Level;
using Code.Runtime.StaticData.Level.MarkersStaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        
        private IStaticDataService _staticDataService;
        private InteractablesFactory _interactablesFactory;

        [MenuItem("Tools/Interactables Preview Spawn")]
        public static void ShowExample()
        {
            InteractablesSpawn window = GetWindow<InteractablesSpawn>();
            window.titleContent = new GUIContent("InteractablesSpawn");
            window._staticDataService = new StaticDataService();
            window._staticDataService.LoadAll();
            window._interactablesFactory = new InteractablesFactory(
                new AssetProviderMock(),
                new SaveLoadRegistry(), 
                new InteractablesRegistry(),
                window._staticDataService,
                new TruckProvider(),
                new CustomersRegistryService());
        }

        private void CreateGUI()
        {
            VisualElement content = _visualTreeAsset.Instantiate();
            rootVisualElement.Add(content);
            _infoBoxText = rootVisualElement.Q<Label>("InfoBoxText");
            _spawnButton = rootVisualElement.Q<Button>("SpawnInteractablesButton");
            _despawnButton = rootVisualElement.Q<Button>("DespawnInteractablesButton");

            _spawnButton.clicked += SpawnInteractableObjectsPreview;
            _despawnButton.clicked += DeleteInteractableObjectsPreview;
            
            UpdateInfoBoxText();
        }

        private void UpdateInfoBoxText() =>
            _infoBoxText.text = $"Currently spawned in preview: {FindPreviewMarkers().Length} interactables";

        private void SpawnInteractableObjectsPreview()
        {
            LevelStaticData levelData = _staticDataService.CurrentLevelData;
            List<GameObject> spawned = new();
            
            SpawnBookSlots(levelData, spawned);
            SpawnReadingTables(levelData, spawned);
            
            spawned.ForEach(x => x.AddComponent<PreviewMarker>());
            UpdateInfoBoxText();
        }

        private void DeleteInteractableObjectsPreview()
        {
            PreviewMarker[] previewMarkers = FindPreviewMarkers();

            foreach(PreviewMarker previewMarker in previewMarkers)
                DestroyImmediate(previewMarker.gameObject);
            
            UpdateInfoBoxText();
        }

        private void SpawnReadingTables(LevelStaticData levelData, List<GameObject> spawned)
        {
            foreach(ReadingTableSpawnData readingTable in levelData.InteractablesSpawns.ReadingTables)
                spawned.Add(_interactablesFactory.CreateReadingTable(
                    readingTable.Id,
                    readingTable.Position,
                    readingTable.Rotation,
                    readingTable.InitialBookId));
        }

        private void SpawnBookSlots(LevelStaticData levelData, List<GameObject> spawned)
        {
            foreach(BookSlotSpawnData spawn in levelData.InteractablesSpawns.BookSlots)
                spawned.Add(
                    _interactablesFactory.CreateBookSlot(spawn));
        }

        private PreviewMarker[] FindPreviewMarkers() =>
            FindObjectsByType<PreviewMarker>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }
}