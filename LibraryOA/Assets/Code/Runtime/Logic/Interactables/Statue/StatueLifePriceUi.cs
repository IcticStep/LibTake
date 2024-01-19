using Code.Runtime.Infrastructure.Services.StaticData;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Statue
{
    internal sealed class StatueLifePriceUi : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _priceText;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        private void Start() =>
            _priceText.text = "x" + _staticDataService.Interactables.Statue.LifeRestorePrice;
    }
}