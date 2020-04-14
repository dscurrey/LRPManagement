using System;
using System.Collections.Generic;

namespace LRPManagement.Models
{
    public partial class Craftable
    {
        public Craftable()
        {
            Bond = new HashSet<Bond>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Bond> Bond { get; set; }
    }
}
