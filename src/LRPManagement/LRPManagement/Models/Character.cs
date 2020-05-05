using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LRPManagement.Models
{
    public partial class Character
    {
        public Character()
        {
            Bond = new HashSet<Bond>();
            CharacterSkills = new HashSet<CharacterSkill>();
        }

        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Current XP")]
        public int Xp { get; set; }

        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }

        [Display(Name = "Is Retired?")]
        public bool IsRetired { get; set; }

        [Display(Name = "Character Reference")]
        public int CharacterRef { get; set; }

        [Display(Name = "Player ID")]
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
        public virtual ICollection<Bond> Bond { get; set; }
        public virtual ICollection<CharacterSkill> CharacterSkills { get; set; }
    }
}