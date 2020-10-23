using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WindowsFront_end.Model
{
    public class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int ItemId { get; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("ItemName"); }
        }

        private bool _isDone;
        public bool IsDone
        {
            get { return _isDone; }
            set { _isDone = value; RaisePropertyChanged("ItemIsDone"); }

        }

        //Misschien niet nodig? 
        //voor mij lijkt het beter da het eenmaal wordt ingesteld en daarna niet meer kan veranderen
        private ItemType _itemType;
        public ItemType ItemType
        {
            get { return _itemType; }
            set { _itemType = value; RaisePropertyChanged("ItemItemType"); }
        }
        public List<ItemCategory> Categories { get; set; } = new List<ItemCategory>();

        public Item(string name)
        {
            Name = name;
            IsDone = false;
        }

        public Item()
        {
        }


        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}