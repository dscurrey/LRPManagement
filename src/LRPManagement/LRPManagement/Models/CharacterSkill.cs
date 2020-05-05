using System.ComponentModel.DataAnnotations;

namespace LRPManagement.Models
{
    public partial class CharacterSkill
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Character ID")]
        public int CharacterId { get; set; }

        [Display(Name = "Skill ID")]
        public int SkillId { get; set; }

        public virtual Character Character { get; set; }
        public virtual Skill Skill { get; set; }
    }
}