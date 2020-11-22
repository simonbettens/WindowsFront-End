using System.Collections.Generic;

namespace WindowsBackend.Models
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<int> Items { get; set; } = new List<int>();
    }
}