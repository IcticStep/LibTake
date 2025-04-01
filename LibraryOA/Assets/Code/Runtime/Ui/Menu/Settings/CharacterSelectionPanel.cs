using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.StaticData.CharacterSelection;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.Menu.Settings
{
    internal sealed class CharacterSelectionPanel : MonoBehaviour
    {
        [SerializeField]
        private List<CharacterSelectionToggle> _toggles;
        
        private ISaveLoadService _saveLoadService;
        private IStaticDataService _staticDataService;
        private Dictionary<CharacterTypeId, CharacterSelectionToggle> _togglesMap = new();

        [Inject]
        private void Construct(ISaveLoadService saveLoadService, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
        }

        private void Awake()
        {
            List<CharacterTypeId> availableCharacters = _staticDataService.GetAllAvailableCharacterTypes().ToList();
            InitializeToggles(availableCharacters);
            CharacterTypeId characterSelected = _saveLoadService.LoadCharacterSelected();
            _togglesMap.TryGetValue(characterSelected, out CharacterSelectionToggle toggle);
            
            if(toggle != null)
                toggle.Select();
            else
                _toggles.First().Select();
        }

        private void InitializeToggles(List<CharacterTypeId> availableCharacters)
        {
            for(int i = 0; i < _toggles.Count; i++)
            {
                CharacterSelectionToggle toggle = _toggles[i];
                
                if(i < availableCharacters.Count)
                {
                    CharacterTypeId character = availableCharacters[i];
                    toggle.Initialize(character);
                    toggle.gameObject.SetActive(true);
                    _togglesMap.Add(character, toggle);
                }
                else
                {
                    toggle.gameObject.SetActive(false);
                }
            }
        }
    }
}