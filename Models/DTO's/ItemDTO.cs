using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WindowsFront_end.Models;

namespace WindowsBackend.Models.DTO_s
{
    public static class ItemDTO
    {
        public class Overview
        {
            public int ItemId { get; set; }
            public string Name { get; set; }
            public ItemType ItemType { get; set; }
            public string Category { get; set; }
            public List<ForItemOverview> Travelers { get; set; }

            public Overview(Item item)
            {
                ItemId = item.ItemId;
                Name = item.Name;
                ItemType = item.ItemType;
                Category = item.Category;
                Travelers = item.Persons.ToList();
            }

        }

        public class ForItemOverview : INotifyPropertyChanged
        {
            public string PersonName { get; set; }
            public string PersonEmail { get; set; }
            private bool _isDone;
            public bool IsDone
            {
                get { return _isDone; }
                set { _isDone = value; RaisePropertyChanged("IsDone"); }
            }
            public ForItemOverview()
            {

            }

            public ForItemOverview(ItemPerson itemPerson)
            {
                PersonEmail = itemPerson.Person.Email;
                PersonName = itemPerson.Person.Name;
                IsDone = itemPerson.IsDone;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            public int ItemType { get; set; }
            public int CategoryId { get; set; }
        }

        public class ForOnePersonOverview : INotifyPropertyChanged
        {
            public int ItemId { get; set; }
            public string Name { get; set; }
            public ItemType ItemType { get; set; }
            public string Category { get; set; }
            public string PersonName { get; set; }
            public string PersonEmail { get; set; }
            private bool _isDone;
            public bool IsDone
            {
                get { return _isDone; }
                set { _isDone = value; RaisePropertyChanged("IsDone"); }
            }
            public int AmountOfPeople { get; set; }
            public ForOnePersonOverview()
            {

            }

            public ForOnePersonOverview(int id, string name, ItemType type, string cat, int amount, ItemDTO.ForItemOverview p)
            {
                this.ItemId = id;
                this.Name = name;
                this.ItemType = type;
                this.Category = cat;
                this.AmountOfPeople = amount;
                PersonEmail = p.PersonEmail;
                PersonName = p.PersonName;
                IsDone = p.IsDone;
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
