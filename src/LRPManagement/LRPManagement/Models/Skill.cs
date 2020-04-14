using System.Collections.Generic;

namespace LRPManagement.Models
{
    public partial class Skill
    {
        public Skill()
        {
            CharacterSkills = new HashSet<CharacterSkill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CharacterSkill> CharacterSkills { get; set; }
    }
}
