using Code.Runtime.Logic.Customers;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.Logic
{
    [CustomEditor(typeof(ICustomerStateMachine))]
    public class CustomerStateMachineEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            ICustomerStateMachine stateMachine = (ICustomerStateMachine)target;
            base.OnInspectorGUI();
            GUILayout.Label($"Current state: {stateMachine.ActiveStateName}");
        }
    }
}