using Code.Runtime.Logic.Interactables.Crafting;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.Logic
{
    [CustomEditor(typeof(CraftingTableStateMachine))]
    internal sealed class CraftingTableStateMachineEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            CraftingTableStateMachine craftingTableStateMachine = (CraftingTableStateMachine)target;
            base.OnInspectorGUI();
            GUILayout.Label($"Current state: {craftingTableStateMachine.ActiveStateName}");
        }
    }
}