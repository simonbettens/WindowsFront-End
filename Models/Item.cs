using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WindowsBackend.Models.DTO_s;

namespace WindowsFront_end.Models
{
    public class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int ItemId { get; set;  }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("ItemName"); }
        }

        //Misschien niet nodig? 
        //voor mij lijkt het beter da het eenmaal wordt ingesteld en daarna niet meer kan veranderen
        private ItemType _itemType;
        public ItemType ItemType
        {
            get { return _itemType; }
            set { _itemType = value; RaisePropertyChanged("ItemItemType"); }
        }
        public string Category { get; set; }

        public List<ItemDTO.ForItemOverview> Persons { get; set; } = new List<ItemDTO.ForItemOverview>();

        public Item(ItemDTO.Overview dto)
        {
            ItemId = dto.ItemId;
            Name = dto.Name;
            ItemType = dto.ItemType;
            Category = dto.Category;
            Persons = dto.Persons;

        }

        public Item(string name)
        {
            Name = name;
        }

        public Item()
        {
        }


        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}