using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRP.Players.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string AccountRef { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateJoined { get; set; }
        public bool IsActive { get; set; }
    }
}
