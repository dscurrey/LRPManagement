using System;

namespace DTO
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public string AccountRef { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
