using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.StaticData;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class SkillStats
    {
        [SerializeField]
        private List<Skill> _skills = new();

        public event Action Updated;
        
        public int GetLevelFor(BookType bookType)
        {
            Skill skill = FindSkill(bookType);
            if(skill is not null)
                return skill.Level;

            AddSkill(bookType);
            return 0;
        }

        public void AddLevelsFor(BookType bookType, int levels)
        {
            Skill skill = FindSkill(bookType);
            if(skill is null)
            {
                AddSkill(bookType);
                skill = FindSkill(bookType);
            }
            
            skill.AddLevels(levels);
            Updated?.Invoke();
        }

        private Skill FindSkill(BookType bookType) =>
            _skills.FirstOrDefault(x => x.BookType == bookType);

        private void AddSkill(BookType bookType) =>
            _skills.Add(new Skill(bookType, 0));
    }
}