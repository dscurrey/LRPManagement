using System.Collections.Generic;

namespace LRP.Skills.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int XpCost { get; set; }

        public virtual List<CharacterSkill> CharacterSkills { get; set; }
    }
}
