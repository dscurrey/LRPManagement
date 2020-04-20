using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Models;

namespace LRPManagement.ViewModels
{
    public class CharacterDetailsViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Player ID")]
        public int PlayerId { get; set; }
        [Display(Name = "Full ID")]
        public string FullId
        {
            get { return Id + "." + PlayerId; }
        }
        [Display(Name = "Character Name")]
        public string Name { get; set; }
        [Display(Name = "Experience Points")]
        public int Xp { get; set; }
        [Display(Name = "Character Active?")]
        public bool IsActive { get; set; }
        [Display(Name = "Skills")]
        public List<Skill> Skills { get; set; }
    }
}
