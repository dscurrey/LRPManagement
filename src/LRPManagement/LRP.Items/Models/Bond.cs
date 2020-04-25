using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRP.Items.Models
{
    public class Bond
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int PlayerId { get; set; }

        public virtual Craftable Item { get; set; }
    }
}
