using System.Collections.Generic;

namespace WindowsFront_end.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<ItemCategory> Items { get; set; }

        public Category(string name)
        {
            Name = name;
        }

        public Category()
        {

        }
    }
}