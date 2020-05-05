using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LRPManagement.Models
{
    public partial class Craftable
    {
        public Craftable()
        {
            Bond = new HashSet<Bond>();
        }

        [Display(Name = "Item ID")]
        public int Id { get; set; }

        [Display(Name = "Item Reference")]
        public int ItemRef { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Form")]
        public string Form { get; set; }

        [Display(Name = "Requirement(s)")]
        public string Requirement { get; set; }

        [Display(Name = "Effect")]
        public string Effect { get; set; }

        [Display(Name = "Material(s)")]
        public string Materials { get; set; }

        public virtual ICollection<Bond> Bond { get; set; }
    }
}