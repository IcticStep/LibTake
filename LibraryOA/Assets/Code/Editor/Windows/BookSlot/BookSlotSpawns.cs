using System;
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

        private ObjectField _containerField;
        private IMGUIContainer _toolBox;
        private Slider _circleRadiusSlider;
        private SliderInt _objectCountSlider;

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
        }

        private void SetToolBoxVisibility() =>
            _toolBox.style.display = HasTarget
                ? DisplayStyle.Flex
                : DisplayStyle.None;
    }
}
