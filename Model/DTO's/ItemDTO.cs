using System.Collections.Generic;

namespace WindowsBackend.Models.DTO_s
{
    public class ItemDTO
    {

        public int ItemId { get; set; }
        public string Name { get; set; }
        //public ItemType ItemType { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public List<ItemPersonDTO> ItemPersons { get; set; }


    }
}
