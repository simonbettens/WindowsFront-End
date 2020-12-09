using System.Collections.Generic;
using System.Linq;
using WindowsFront_end.Model;

namespace WindowsBackend.Models
{
    public static class CategoryDTO
    {
        public class Create
        {
            public string Name { get; set; }
        }

        public class Overview
        {
            public int CategoryId { get; set; }
            public string Name { get; set; }
            public List<int> Items { get; set; } = new List<int>();

            public Overview(Category category)
            {
                CategoryId = category.CategoryId;
                Name = category.Name;
                Items = category.Items.Select(i => i.ItemId).ToList();
            }
        }

        public class Basic
        {
            public int CategoryId { get; set; }
            public string Name { get; set; }

            public Basic(Category category)
            {
                CategoryId = category.CategoryId;
                Name = category.Name;
            }

        }
    }
}