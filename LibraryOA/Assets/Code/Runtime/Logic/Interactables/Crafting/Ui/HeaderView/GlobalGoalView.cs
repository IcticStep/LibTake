using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.StaticData.GlobalGoals;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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

        [SerializeField]
        private Canvas _mainCanvas;
        
        [SerializeField]
        private CraftingTableStateMachine _craftingTableStateMachine;
        
        private ICraftingService _craftingService;

        [Inject]
        private void Construct(ICraftingService craftingService) =>
            _craftingService = craftingService;
        
        private void Awake() =>
            _craftingTableStateMachine.EnterState += OnStateEntered;

        private void OnDestroy() =>
            _craftingTableStateMachine.EnterState -= OnStateEntered;

        private void OnStateEntered(ICraftingTableState state)
        {
            if(state is PayState && _craftingService.FinishedGoal)
                _mainCanvas.enabled = false;
        }

        private void Start()
        {
            GlobalGoal goal = _craftingService.Goal;
            _icon.sprite = goal.Icon;
            _headerText.text = goal.Name;
        }
    }
}