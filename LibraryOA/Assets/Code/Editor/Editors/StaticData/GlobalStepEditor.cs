using System;
using System.Linq;
using Code.Runtime.Logic;
using Code.Runtime.StaticData.Books;
using Code.Runtime.StaticData.GlobalGoals;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.StaticData
{
    [CustomEditor(typeof(GlobalStep))]
    internal sealed class GlobalStepEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("Create skill constraits"))
                CreateSkillConstraints();
        }

        private void CreateSkillConstraints()
        {
            GlobalStep globalStep = (GlobalStep)target;
            BookType[] bookTypes = (BookType[])Enum.GetValues(typeof(BookType));

            foreach(BookType bookType in bookTypes)
            {
                if(globalStep.LevelRequirements.All(skill => skill.BookType != bookType))
                    globalStep.LevelRequirements.Add(new SkillConstraint(bookType));
            }
        }
    }
}