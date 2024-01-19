using System.Linq;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Skills;
using Code.Runtime.StaticData.Books;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui.HudComponents
{
    public sealed class SkillView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private Image _iconImage;
        [SerializeField]
        private BookType _bookType;
        
        private ISkillService _skillService;
        private IStaticDataService _staticDataService;
        private StaticBookType _staticBookType;

        [Inject]
        private void Construct(ISkillService skillService, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _skillService = skillService;
        }

        private void OnValidate() =>
            _text ??= GetComponentInChildren<TextMeshProUGUI>();

        private void Awake() =>
            _staticBookType = _staticDataService.BookTypes.First(type => type.BookType == _bookType);

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
            _iconImage.sprite = _staticBookType.Icon;
        }
    }
}