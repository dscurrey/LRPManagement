using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LRPManagement.Models
{
    public partial class Skill
    {
        public Skill()
        {
            CharacterSkills = new HashSet<CharacterSkill>();
        }

        [Display(Name = "Skill ID")]
        public int Id { get; set; }

        [Display(Name = "Skill Reference")]
        public int SkillRef { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "XP Cost")]
        public int XpCost { get; set; }

        public virtual ICollection<CharacterSkill> CharacterSkills { get; set; }
    }
}