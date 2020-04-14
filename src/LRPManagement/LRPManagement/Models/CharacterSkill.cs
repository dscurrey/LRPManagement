using System;
using System.Collections.Generic;
using LRPManagement.Models;

namespace LRPManagement.Models
{
    public partial class CharacterSkill
    {
        public int CharacterId { get; set; }
        public int SkillId { get; set; }

        public virtual Character Character { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
