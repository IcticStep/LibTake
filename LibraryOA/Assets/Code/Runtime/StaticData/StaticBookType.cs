using System;
using UnityEngine;

namespace Code.Runtime.StaticData
{
    [CreateAssetMenu(fileName = "BookType", menuName = "Static data/Book Type")]
    public sealed class StaticBookType : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField]
        public Material Material { get; private set; }
    }
}