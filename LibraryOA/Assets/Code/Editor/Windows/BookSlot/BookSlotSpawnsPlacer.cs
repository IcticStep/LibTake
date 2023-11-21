using System.Collections.Generic;
using System.Linq;
using Code.Editor.Editors;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Markers;
using Code.Runtime.Logic.Markers.Spawns;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Editor.Windows.BookSlot
{
    public class BookSlotSpawnsPlacer : EditorWindow
    {
        private const string ContainerFieldName = "ContainerField";
        private const string ToolBoxName = "ToolBox";
        private const string CircleRadiusSliderName = "CircleRaidusSlider";
        private const string ObjectCountSliderName = "ObjectCountsSlider";
        private const string SkipObjectSliderName = "ObjectSkipSlider";
        private const string InfoLabelName = "InfoLabel";

        private readonly UniqueIdUpdater _uniqueIdUpdater = new();
        
        [SerializeField]
        private VisualTreeAsset _visualTreeAsset;
        [SerializeField]
        private BookSlotSpawn _bookSlotSpawnPrefab;

        private ObjectField _containerField;
        private IMGUIContainer _toolBox;
        private Slider _circleRadiusSlider;
        private SliderInt _objectCountSlider;
        private SliderInt _skipObjectSlider;
        private Label _infoLabel;

        private List<BookSlotSpawn> _spawns;
        private bool _initialized;

        private BookSlotSpawnContainer Container => _containerField.value as BookSlotSpawnContainer;
        private float CircleRadius => _circleRadiusSlider.value;
        private int TargetSpawnsCount => _objectCountSlider.value - ObjectsToSkip;
        private int ObjectsToSkip => _skipObjectSlider.value;
        private bool HasTarget => _containerField.value != null;

        [MenuItem("Tools/BookSlotSpawns")]
        public static void OpenWindow() =>
            GetWindow<BookSlotSpawnsPlacer>();

        public void CreateGUI()
        {
            VisualElement gui = _visualTreeAsset.Instantiate();
            VisualElement root = rootVisualElement;
            root.Add(gui);
            
            _containerField = root.Q<ObjectField>(ContainerFieldName);
            _toolBox = root.Q<IMGUIContainer>(ToolBoxName);
            _circleRadiusSlider = root.Q<Slider>(CircleRadiusSliderName);
            _objectCountSlider = root.Q<SliderInt>(ObjectCountSliderName);
            _skipObjectSlider = root.Q<SliderInt>(SkipObjectSliderName);
            _infoLabel = root.Q<Label>(InfoLabelName);
        }

        private void OnGUI()
        {
            if(Application.isPlaying)
                return;
            
            SetToolBoxVisibility();
            if(!HasTarget)
            {
                _initialized = false;
                return;
            }

            InitializeWindow();
            UpdateContainer();
            AdjustObjectsCount();
            SetObjectsInCircle();
            UpdateInfoUi();
        }

        private void SetToolBoxVisibility() =>
            _toolBox.style.display = HasTarget
                ? DisplayStyle.Flex
                : DisplayStyle.None;

        private void InitializeWindow()
        {
            if(_initialized)
                return;
            
            _spawns = Container.GetComponentsInChildren<BookSlotSpawn>().ToList();
            _circleRadiusSlider.value = Container.CircleRadius;
            _skipObjectSlider.value = Container.ObjectsToSkip;
            _objectCountSlider.value = _spawns.Count + ObjectsToSkip;
            _initialized = true;
        }

        private void UpdateContainer()
        {
            Container.CircleRadius = CircleRadius;
            Container.ObjectsToSkip = ObjectsToSkip;
        }

        private void AdjustObjectsCount()
        {
            AddIfLess();
            RemoveIfMore();
        }

        private void SetObjectsInCircle()
        {
            float angleStep = 2 * Mathf.PI / (TargetSpawnsCount + ObjectsToSkip);
            Vector3 containerPosition = Container.transform.position;
            
            for (int i = 0; i < TargetSpawnsCount; i++)
            {
                float angle = (i + ObjectsToSkip) * angleStep;
                Vector3 position = new(
                    Mathf.Cos(angle) * CircleRadius,
                    0f,
                    Mathf.Sin(angle) * CircleRadius
                );

                _spawns[i].transform.localPosition = position;
                _spawns[i].transform.LookAt(containerPosition);
            }
        }

        private void UpdateInfoUi()
        {
            float circleLength = 2 * Mathf.PI * CircleRadius;
            int totalObjectsPlaces = TargetSpawnsCount + ObjectsToSkip;
            _infoLabel.text = $"Circle length: {circleLength}\n" +
                              $"Length per object: {circleLength / totalObjectsPlaces}";
        }

        private void AddIfLess()
        {
            for(int i = _spawns.Count; i < TargetSpawnsCount; i++)
                AddObject();
        }

        private void RemoveIfMore()
        {
            for(int i = _spawns.Count; i > TargetSpawnsCount && i > 0; i--)
                RemoveObject();
        }

        private void AddObject()
        {
            BookSlotSpawn spawn = PrefabUtility
                .InstantiatePrefab(_bookSlotSpawnPrefab, Container.transform)
                .GetComponentInChildren<BookSlotSpawn>();

            UniqueId uniqueId = spawn.GetComponentInChildren<UniqueId>();
            _uniqueIdUpdater.UpdateUniqueId(uniqueId);
            
            _spawns.Add(spawn);
        }

        private void RemoveObject()
        {
            BookSlotSpawn last = _spawns[^1];
            DestroyImmediate(last.gameObject);
            _spawns.RemoveAt(_spawns.Count-1);
        }
    }
}
