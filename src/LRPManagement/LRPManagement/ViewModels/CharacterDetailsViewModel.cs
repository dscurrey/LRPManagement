using LRPManagement.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LRPManagement.ViewModels
{
    public class CharacterDetailsViewModel
    {
        [Display(Name = "ID")] public int Id { get; set; }
        [Display(Name = "Player ID")] public int PlayerId { get; set; }
        [Display(Name = "Full ID")] public string FullId => Id + "." + PlayerId;

        [Display(Name = "Character Name")] public string Name { get; set; }
        [Display(Name = "Experience Points")] public int Xp { get; set; }
        [Display(Name = "Character Active?")] public bool IsActive { get; set; }
        [Display(Name = "Skills")] public IEnumerable<Bond> Items { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
    }
}