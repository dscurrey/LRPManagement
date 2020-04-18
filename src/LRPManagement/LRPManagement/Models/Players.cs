using System;
using System.Collections.Generic;

namespace LRPManagement.Models
{
    public partial class Player
    {
        public Player()
        {
            Characters = new HashSet<Character>();
        }

        public int Id { get; set; }
        public string AccountRef { get; set; }
        public int PlayerRef { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
    }
}
