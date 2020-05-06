using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LRPManagement.Models
{
    public partial class Player
    {
        public Player()
        {
            Characters = new HashSet<Character>();
        }

        [Display(Name = "Player ID")]
        public int Id { get; set; }

        [Display(Name = "Account Reference")]
        public string AccountRef { get; set; }

        [Display(Name = "Player Reference")]
        public int PlayerRef { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
    }
}