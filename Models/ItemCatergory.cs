namespace WindowsFront_end.Model
{
    public class ItemCategory
    {
        public int Id { get; }
        public Item Item { get; set; }
        public Category Category { get; set; }
    }
}
