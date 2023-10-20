using System;
using Code.Runtime.Utils;
using UnityEngine;

namespace Code.Runtime.StaticData
{
    [CreateAssetMenu(fileName = "Book", menuName = "Static data/Book")]
    public class StaticBook : ScriptableObject
    {
        [field: ReadOnly, SerializeField] 
        public string Id { get; private set; } = Guid.NewGuid().ToString();

        [field: SerializeField]
        public string Title { get; private set; }

        [field: SerializeReference]
        public StaticBookType StaticBookType { get; private set; }
    }
}