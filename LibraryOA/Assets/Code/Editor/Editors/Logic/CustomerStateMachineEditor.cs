using Code.Runtime.Logic.Customers;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.Logic
{
    [CustomEditor(typeof(CustomerStateMachine))]
    public class CustomerStateMachineEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            CustomerStateMachine stateMachine = (CustomerStateMachine)target;
            base.OnInspectorGUI();
            GUILayout.Label($"Current state: {stateMachine.ActiveStateName}");
        }
    }
}