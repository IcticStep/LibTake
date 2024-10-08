using System.Linq;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.StaticData.Books;
using UnityEditor;
using UnityEngine;

namespace Code.Runtime.Ui.HudComponents
{
    internal sealed class SkillsPanel : MonoBehaviour
    {
#if UNITY_EDITOR
        [ContextMenu("Update Editor Views")]
        private void UpdateEditorViews()
        {
            SkillView[] skillViews = GetComponentsInChildren<SkillView>();
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadAll();

            foreach(SkillView skillView in skillViews)
            {
                StaticBookType staticBookType = staticDataService.BookTypes.First(type => type.BookType == skillView.BookType);
                skillView.IconImage.sprite = staticBookType.Icon;
                skillView.BorderImage.color = staticBookType.UiTextColor;
                skillView.IconImage.color = staticBookType.UiTextColor;
                skillView.Text.color = staticBookType.UiTextColor;
                
                EditorUtility.SetDirty(skillView.gameObject);
            }
        }
#endif
    }
}