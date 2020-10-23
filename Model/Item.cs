using System.Collections.Generic;
namespace WindowsFront_end.Model
{
    public class Item
    {
        public int ItemId { get; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public ItemType ItemType { get; set; }
        public List<ItemCategory> Categories { get; set; } = new List<ItemCategory>();

        public Item(string name)
        {
            Name = name;
            IsDone = false;
        }

        public Item()
        {
        }
    }
}