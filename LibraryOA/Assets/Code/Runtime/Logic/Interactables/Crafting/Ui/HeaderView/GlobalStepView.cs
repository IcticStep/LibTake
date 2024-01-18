using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.StaticData.GlobalGoals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Crafting.Ui.HeaderView
{
    internal sealed class GlobalStepView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TextMeshProUGUI _headerText;

        [SerializeField]
        private TextMeshProUGUI _stepIndexText;

        [SerializeField]
        private CraftingTableStateMachine _craftingTableStateMachine;
        
        private ICraftingService _craftingService;

        [Inject]
        private void Construct(ICraftingService craftingService) =>
            _craftingService = craftingService;

        private void Awake() =>
            _craftingTableStateMachine.EnterState += OnStateEntered;

        private void Start() =>
            UpdateView();

        private void OnDestroy() =>
            _craftingTableStateMachine.EnterState -= OnStateEntered;

        private void OnStateEntered(ICraftingTableState state) =>
            UpdateView();

        private void UpdateView()
        {
            if(_craftingService.FinishedGoal)
                return;

            VisualizeStep();
        }

        private void VisualizeStep()
        {
            GlobalStep step = _craftingService.CurrentStep;
            _icon.sprite = step.Icon;
            _headerText.text = step.Name;
            _stepIndexText.text = $"{_craftingService.CurrentStepIndex + 1}/{_craftingService.Goal.GlobalSteps.Count}";
        }
    }
}