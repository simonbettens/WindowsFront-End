using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFront_end.Model
{
    public class ItemPerson
    {

        public int Id { get; }
        public Item Item { get; set; }
        public Person Person { get; set; }
        public bool IsDone { get; set; } = false;

    }
}
