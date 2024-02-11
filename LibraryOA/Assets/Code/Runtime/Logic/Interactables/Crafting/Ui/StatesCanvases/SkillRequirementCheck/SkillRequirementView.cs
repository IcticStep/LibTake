using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Skills;
using Code.Runtime.StaticData.Books;
using Code.Runtime.StaticData.GlobalGoals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Crafting.Ui.StatesCanvases.SkillRequirementCheck
{
    internal sealed class SkillRequirementView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private Image _iconImage;
        [SerializeField]
        private BookType _bookType;
        [SerializeField]
        private SkillRequirementsCanvasView _skillRequirementsCanvasView;
        
        private IStaticDataService _staticDataService;
        private StaticBookType _staticBookType;
        private IPlayerSkillService _playerSkillService;

        private Sprite CompletedIcon => _staticDataService.Ui.CompletedIcon;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IPlayerSkillService playerSkillService)
        {
            _playerSkillService = playerSkillService;
            _staticDataService = staticDataService;
        }

        private void OnValidate()
        {
            _iconImage ??= GetComponentInChildren<Image>();
            _text ??= GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Awake()
        {
            _staticBookType = _staticDataService.BookTypes.First(type => type.BookType == _bookType);
            _skillRequirementsCanvasView.RequirementsRequested += OnRequirementsRequested;
        }

        private void OnDestroy() =>
            _skillRequirementsCanvasView.RequirementsRequested -= OnRequirementsRequested;
        
        private void OnRequirementsRequested(IReadOnlyList<SkillConstraint> requirements)
        {
            SkillConstraint requirement = requirements.First(requirement => requirement.BookType == _bookType);
            int level = _playerSkillService.GetSkillByBookType(requirement.BookType);

            if(level >= requirement.RequiredLevel)
            {
                ShowRequirementCompleted();
                return;
            }
            
            ShowRequirement(requirement);
        }

        private void ShowRequirement(SkillConstraint requirement)
        {
            string levelText = requirement.RequiredLevel.ToString();
            _text.enabled = true;
            _text.text = levelText;
            _iconImage.sprite = _staticBookType.Icon;
        }

        private void ShowRequirementCompleted()
        {
            _text.enabled = false;
            _iconImage.sprite = CompletedIcon;
        }
    }
}