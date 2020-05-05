using System.ComponentModel.DataAnnotations;

namespace LRPManagement.Models
{
    public partial class Bond
    {
        [Display(Name = "Bond ID")]
        public int Id { get; set; }

        [Display(Name = "Character ID")]
        public int CharacterId { get; set; }

        [Display(Name = "Item ID")]
        public int ItemId { get; set; }

        public virtual Character Character { get; set; }
        public virtual Craftable Item { get; set; }
    }
}