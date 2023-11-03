using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Logic.SpawnMarkers;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Editor.Windows.BookSlot
{
    public class BookSlotSpawns : EditorWindow
    {
        private const string ContainerFieldName = "ContainerField";
        private const string ToolBoxName = "ToolBox";
        private const string CircleRadiusSliderName = "CircleRaidusSlider";
        private const string ObjectCountSliderName = "ObjectCountsSlider";

        [SerializeField]
        private VisualTreeAsset _visualTreeAsset;
        [SerializeField]
        private BookSlotSpawn _bookSlotSpawnPrefab;

        private ObjectField _containerField;
        private IMGUIContainer _toolBox;
        private Slider _circleRadiusSlider;
        private SliderInt _objectCountSlider;
        private List<BookSlotSpawn> _spawns;

        private BookSlotSpawnContainer Container => _containerField.value as BookSlotSpawnContainer;
        private float CircleRadius => _circleRadiusSlider.value;
        private float TargetSpawnsCount => _objectCountSlider.value;
        private bool HasTarget => _containerField.value != null;

        [MenuItem("Tools/BookSlotSpawns")]
        public static void ShowExample() =>
            GetWindow<BookSlotSpawns>();

        public void CreateGUI()
        {
            VisualElement gui = _visualTreeAsset.Instantiate();
            VisualElement root = rootVisualElement;
            root.Add(gui);

            _containerField = root.Q<ObjectField>(ContainerFieldName);
            _toolBox = root.Q<IMGUIContainer>(ToolBoxName);
            _circleRadiusSlider = root.Q<Slider>(CircleRadiusSliderName);
            _objectCountSlider = root.Q<SliderInt>(ObjectCountSliderName);
        }

        private void OnGUI()
        {
            SetToolBoxVisibility();
            if(!HasTarget)
                return;

            AdjustObjectsCount();
            SetObjectsInCircle();
        }

        private void SetToolBoxVisibility() =>
            _toolBox.style.display = HasTarget
                ? DisplayStyle.Flex
                : DisplayStyle.None;

        private void AdjustObjectsCount()
        {
            _spawns ??= Container.GetComponentsInChildren<BookSlotSpawn>().ToList();

            AddIfLess();
            RemoveIfMore();
        }

        private void SetObjectsInCircle()
        {
            float angleStep = 2 * Mathf.PI / TargetSpawnsCount;
            Vector3 containerPosition = Container.transform.position;
            
            for (int i = 0; i < TargetSpawnsCount; i++)
            {
                float angle = i * angleStep;
                Vector3 position = new(
                    Mathf.Cos(angle) * CircleRadius,
                    0f,
                    Mathf.Sin(angle) * CircleRadius
                );

                _spawns[i].transform.localPosition = position;
                _spawns[i].transform.LookAt(containerPosition);
            }
        }

        private void AddIfLess()
        {
            for(int i = _spawns.Count; i < TargetSpawnsCount; i++)
                AddObject();
        }

        private void RemoveIfMore()
        {
            for(int i = _spawns.Count; i > TargetSpawnsCount; i--)
                RemoveObject();
        }

        private void AddObject()
        {
            BookSlotSpawn spawn = PrefabUtility
                .InstantiatePrefab(_bookSlotSpawnPrefab, Container.transform)
                .GetComponentInChildren<BookSlotSpawn>();
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
