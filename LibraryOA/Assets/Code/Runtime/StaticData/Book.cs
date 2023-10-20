using UnityEngine;

namespace Code.Runtime.StaticData
{
    [CreateAssetMenu(fileName = "Book", menuName = "Static data/Book")]
    public class Book : ScriptableObject
    {
        [field: SerializeField]
        public string Title { get; set; }

        [field: SerializeReference]
        public BookType BookType { get; set; }
    }
}