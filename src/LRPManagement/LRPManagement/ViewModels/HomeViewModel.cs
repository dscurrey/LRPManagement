using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.ViewModels
{
    public class HomeViewModel
    {
        [Display(Name = "Player Count")]
        public int PlayerCount { get; set; }

        [Display(Name = "Character Count")]
        public int CharacterCount { get; set; }

        [Display(Name = "Skill Count")]
        public int SkillCount { get; set; }

        [Display(Name = "Item Count")]
        public int ItemCount { get; set; }
    }
}
