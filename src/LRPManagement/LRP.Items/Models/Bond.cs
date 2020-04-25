namespace LRP.Items.Models
{
    public class Bond
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int CharacterId { get; set; }

        public virtual Craftable Item { get; set; }
    }
}
