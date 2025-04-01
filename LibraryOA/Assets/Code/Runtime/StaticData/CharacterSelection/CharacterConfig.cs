using UnityEngine;

namespace Code.Runtime.StaticData.CharacterSelection
{
    [CreateAssetMenu(fileName = "CharactersSelectionConfig", menuName = "Static data/Characters Selection Config")]
    public sealed class CharacterConfig : ScriptableObject
    {
        public CharacterTypeId Type;
        public SexTypeId Sex;
        public Sprite Icon;
        public Material Material;
    }
}