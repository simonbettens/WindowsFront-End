using System.Collections.Generic;
using System.Linq;
using WindowsFront_end.Model;

namespace WindowsBackend.Models.DTO_s
{
    public static class ItemDTO
    {
        public class Overview
        {
            public int ItemId { get; set; }
            public string Name { get; set; }
            public ItemType ItemType { get; set; }
            public List<string> Categories { get; set; } = new List<string>();
            public List<ForItemOverview> Persons { get; set; }

            public Overview(Item item)
            {
                ItemId = item.ItemId;
                Name = item.Name;
                ItemType = item.ItemType;
                Categories = item.Categories;
                Persons = item.Travelers.ToList();
            }

        }

        public class ForItemOverview
        {
            public string PersonName { get; set; }
            public string PersonEmail { get; set; }
            public bool IsDone { get; set; }

            public ForItemOverview(ItemPerson itemPerson)
            {
                PersonEmail = itemPerson.Person.Email;
                PersonName = itemPerson.Person.Name;
                IsDone = itemPerson.IsDone;
            }
        }

        public class ForPersonOverview
        {
            public int ItemId { get; set; }
            public string Name { get; set; }
            public bool IsDone { get; set; }

            public ForPersonOverview(ItemPerson itemPerson)
            {
                ItemId = itemPerson.Item.ItemId;
                Name = itemPerson.Item.Name;
                IsDone = itemPerson.IsDone;
            }
        }

        public class Create
        {
            public string Name { get; set; }
            public ItemType ItemType { get; set; }
        }



    }
}
