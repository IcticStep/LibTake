using System.Collections.Generic;
using System.Linq;
using Code.Runtime.StaticData.Books;
using Code.Runtime.StaticData.GlobalGoals;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.StaticData
{
    [CustomEditor(typeof(GlobalGoal))]
    internal sealed class GlobalGoalEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DrawSkillRequirementsForSteps();
        }

        private void DrawSkillRequirementsForSteps()
        {
            GlobalGoal globalGoal = (GlobalGoal)target;
            
            GUILayout.Label("Skills requirements");
            List<GlobalStep> previousSteps = new();
            
            foreach(GlobalStep globalStep in globalGoal.GlobalSteps)
            {
                DrawStep(globalStep, previousSteps);
                previousSteps.Add(globalStep);
            }
        }

        private void DrawStep(GlobalStep globalStep, List<GlobalStep> previousSteps)
        {
            EditorGUI.indentLevel++;
            int delta = GetDeltaForStep(globalStep, previousSteps);
            string displayDelta = GetDeltaDisplay(delta);
            
            EditorGUILayout.LabelField($"{globalStep.Name} {displayDelta}");

            EditorGUI.indentLevel++;
            DrawGlobalStepSkillRequirements(globalStep, previousSteps);

            EditorGUI.indentLevel -= 2;
        }

        private void DrawGlobalStepSkillRequirements(GlobalStep globalStep, List<GlobalStep> previousSteps)
        {
            EditorGUILayout.BeginHorizontal();
            
            EditorGUIUtility.labelWidth = 110;
            
            foreach(SkillConstraint skillRequirement in globalStep.SkillRequirements)
            {
                EditorGUILayout.BeginVertical();

                int delta = GetDeltaForSkill(skillRequirement, previousSteps);
                string deltaDisplay = GetDeltaDisplay(delta);
                EditorGUILayout.LabelField(skillRequirement.BookType.ToString(), $"{skillRequirement.RequiredLevel} {deltaDisplay}");
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.Space(7);
            
            EditorGUILayout.EndHorizontal();
        }

        private int GetDeltaForStep(GlobalStep globalStep, List<GlobalStep> previousSteps)
        {
            if(previousSteps is null || !previousSteps.Any())
                return globalStep
                    .SkillRequirements
                    .Sum(skill => skill.RequiredLevel);

            return globalStep
                .SkillRequirements
                .Sum(skillRequirement => GetDeltaForSkill(skillRequirement, previousSteps));
        }

        private int GetDeltaForSkill(SkillConstraint skillRequirement, List<GlobalStep> previousSteps)
        {
            int currentRequirement = skillRequirement.RequiredLevel;
            int maxPreviousRequirement = GetMaxSkillRequirement(previousSteps, skillRequirement.BookType);
            int delta = Mathf.Max(currentRequirement - maxPreviousRequirement, 0);
            return delta;
        }

        private static int GetMaxSkillRequirement(List<GlobalStep> previousSteps, BookType type)
        {
            if(previousSteps is null || !previousSteps.Any())
                return 0;
            
            return previousSteps
                .Max(step => step
                    .SkillRequirements
                    .First(x => x.BookType == type)
                    .RequiredLevel);
        }

        private static string GetDeltaDisplay(int delta) =>
            delta > 0 ? $"(+{delta})" : string.Empty;
    }
}