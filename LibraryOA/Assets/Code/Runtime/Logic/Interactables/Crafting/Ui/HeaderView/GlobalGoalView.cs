using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.StaticData.GlobalGoals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Crafting.Ui.HeaderView
{
    internal sealed class GlobalGoalView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TextMeshProUGUI _headerText;
        
        private ICraftingService _craftingService;

        [Inject]
        public void Construct(ICraftingService craftingService) =>
            _craftingService = craftingService;

        private void Start()
        {
            GlobalGoal goal = _craftingService.Goal;
            _icon.sprite = goal.Icon;
            _headerText.text = goal.Name;
        }
    }
}