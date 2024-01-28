using System;
using Code.Runtime.Data;
using UnityEngine;
using UnityEngine.Localization;

namespace Code.Runtime.StaticData.Books
{
    [CreateAssetMenu(fileName = "Book", menuName = "Static data/Books/Book")]
    public class StaticBook : ScriptableObject
    {
        [field: ReadOnly]
        [field: SerializeField] 
        public string Id { get; private set; } = Guid.NewGuid().ToString();

        [field: SerializeField]
        public string Title { get; set; }
        
        [field: SerializeField]
        public LocalizedString LocalizedTitle { get; set; }

        [field: SerializeReference]
        public StaticBookType StaticBookType { get; set; }
        
        
    }
}