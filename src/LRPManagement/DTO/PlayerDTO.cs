using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class PlayerDTO
    {
        [Display(Name = "Player ID")]
        public int Id { get; set; }

        [Display(Name = "Account Reference")]
        public string AccountRef { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }

        [Display(Name = "Date Joined")]
        public DateTime DateJoined { get; set; }
    }
}
