using System;
using System.Collections.Generic;

namespace LRPManagement.Models
{
    public partial class Character
    {
        public Character()
        {
            Bond = new HashSet<Bond>();
            CharacterSkills = new HashSet<CharacterSkill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Xp { get; set; }
        public bool IsActive { get; set; }
        public bool IsRetired { get; set; }
        public int CharacterRef { get; set; }
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
        public virtual ICollection<Bond> Bond { get; set; }
        public virtual ICollection<CharacterSkill> CharacterSkills { get; set; }
    }
}
