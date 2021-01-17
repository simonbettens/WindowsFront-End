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

        private ItemType _itemType;
        public ItemType ItemType
        {
            get { return _itemType; }
            set { _itemType = value; RaisePropertyChanged("ItemItemType"); }
        }
        public Category Category { get; set; }

        public List<ItemDTO.ForItemOverview> Persons { get; set; } = new List<ItemDTO.ForItemOverview>();

        public Trip Trip { get; set; }

        public Item(ItemDTO.Overview dto)
        {
            ItemId = dto.ItemId;
            Name = dto.Name;
            ItemType = dto.ItemType;
            Category = new Category(dto.Category);
            Persons = dto.Persons;

        }

        public Item(ItemDTO.ForPersonOverview dto)
        {
            ItemId = dto.ItemId;
            Name = dto.Name;
            Category = new Category(dto.Category);
            Trip = new Trip(dto.Trip);
            ItemType = dto.ItemType;


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