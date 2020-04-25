using System.Collections.Generic;

namespace LRP.Items.Models
{
    public class Craftable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Form { get; set; }
        public string Requirement { get; set; }
        public string Effect { get; set; }
        public string Materials { get; set; }

        public virtual List<Bond> Bonds { get; set; }
    }
}
