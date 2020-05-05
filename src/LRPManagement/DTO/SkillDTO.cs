using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class SkillDTO
    {
        [Display(Name = "Skill ID")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "XP Cost")]
        public int XpCost { get; set; }
    }
}
