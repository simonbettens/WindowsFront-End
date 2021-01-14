using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsBackend.Models.DTO_s;

namespace WindowsFront_end.Models
{
    public class ItemPerson
    {

        public int Id { get; }
        public Item Item { get; set; }
        public Person Person { get; set; }
        public bool IsDone { get; set; } = false;

        public ItemPerson(ItemDTO.ForPersonOverview i)
        {
            Item = new Item(i.Name);
            IsDone = i.IsDone;
        }

    }
}
