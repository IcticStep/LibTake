using UnityEngine;

namespace Code.Runtime.StaticData.Books
{
    [CreateAssetMenu(fileName = "BookType", menuName = "Static data/Book Type")]
    public sealed class StaticBookType : ScriptableObject
    {
        [field: SerializeField]
        public BookType BookType { get; private set; }
        [field: SerializeField]
        public Material Material { get; private set; }
    }
}