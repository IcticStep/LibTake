using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.Services.StaticData;
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

        [Inject]
        private void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        private void OnValidate()
        {
            _iconImage ??= GetComponentInChildren<Image>();
            _text ??= GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Awake()
        {
            _staticBookType = _staticDataService.BookTypes.First(type => type.BookType == _bookType);
            _skillRequirementsCanvasView.RequirementsUpdated += OnRequirementsUpdated;
        }

        private void OnDestroy() =>
            _skillRequirementsCanvasView.RequirementsUpdated -= OnRequirementsUpdated;
        
        private void OnRequirementsUpdated(IReadOnlyList<SkillConstraint> requirements)
        {
            SkillConstraint level = requirements.First(requirement => requirement.BookType == _bookType);
            string textText = level.ToString();
            _text.text = textText;
            _iconImage.sprite = _staticBookType.Icon;
        }
    }
}