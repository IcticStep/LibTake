using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.StaticData.CharacterSelection;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Player
{
    internal sealed class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private SkinnedMeshRenderer MaleMesh;
        [SerializeField]
        private SkinnedMeshRenderer FemaleMesh;
        
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        public void SetCharacter(CharacterTypeId type)
        {
            CharacterConfig config = _staticDataService.ForCharacter(type);
            MaleMesh.enabled = config.Sex is SexTypeId.Male;
            FemaleMesh.enabled = config.Sex is SexTypeId.Female;

            if(config.Sex is SexTypeId.Male)
                MaleMesh.sharedMaterial = config.Material;
            else
                FemaleMesh.sharedMaterial = config.Material;
        }
    }
}