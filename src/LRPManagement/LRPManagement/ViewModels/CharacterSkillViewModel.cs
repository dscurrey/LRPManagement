using System.ComponentModel.DataAnnotations;

namespace LRPManagement.ViewModels
{
    public class CharacterSkillViewModel
    {
        [Display(Name = "Character ID")] public int CharId { get; set; }
        [Display(Name = "Skill ID")] public int SkillId { get; set; }
    }
}