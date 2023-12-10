using System;
using Code.Runtime.Services.Skills;
using Code.Runtime.StaticData.Books;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace Code.Runtime.Ui.Hud
{
    public sealed class SkillView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private BookType _bookType;
        
        private ISkillService _skillService;

        [Inject]
        private void Construct(ISkillService skillService) =>
            _skillService = skillService;

        private void OnValidate() =>
            _text ??= GetComponentInChildren<TextMeshProUGUI>();

        private void Start()
        {
            _skillService.Updated += UpdateView;
            UpdateView();
        }

        private void OnDestroy() =>
            _skillService.Updated -= UpdateView;

        private void UpdateView()
        {
            int skill = _skillService.GetSkillByBookType(_bookType);
            string textText = skill.ToString();
            _text.text = textText;
        }
    }
}