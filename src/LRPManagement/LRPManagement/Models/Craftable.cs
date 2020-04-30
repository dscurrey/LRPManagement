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
        public int ItemRef { get; set; }
        public string Name { get; set; }
        public string Form { get; set; }
        public string Requirement { get; set; }
        public string Effect { get; set; }
        public string Materials { get; set; }

        public virtual ICollection<Bond> Bond { get; set; }
    }
}
