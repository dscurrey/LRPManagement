using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlayerId { get; set; }
        public bool IsActive { get; set; }
        public bool IsRetired { get; set; }

        public virtual Player Player { get; set; }
    }
}
