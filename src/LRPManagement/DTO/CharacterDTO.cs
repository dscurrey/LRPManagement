using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class CharacterDTO
    {
        [Display(Name = "Character ID")]
        public int Id { get; set; }

        [Display(Name = "Player ID")]
        public int PlayerId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Available XP")]
        public int Xp { get; set; }

        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }

        [Display(Name = "Is Retired?")]
        public bool IsRetired { get; set; }
    }
}
