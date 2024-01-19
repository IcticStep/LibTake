using System;
using Code.Runtime.StaticData.Books;
using UnityEngine;

namespace Code.Runtime.StaticData.GlobalGoals
{
    [Serializable]
    public sealed class SkillConstraint
    {
        [SerializeField]
        private BookType _bookType;

        [SerializeField]
        private int _requiredLevel;
        
        public BookType BookType => _bookType;
        public int RequiredLevel => _requiredLevel;

        public SkillConstraint(BookType bookType) 
        {
            _bookType = bookType;
        }
    }
}