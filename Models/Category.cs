using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WindowsFront_end.Models.DTO_s;

namespace WindowsFront_end.Models
{
    public class Category : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int CategoryId { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("CategoryName"); }
        }

        public List<Item> Items { get; set; } = new List<Item>();

        public Category(string name)
        {
            Name = name;
        }

        public Category()
        {


        }

        public Category(CategoryDTO.Overview dto)
        {
            Name = dto.Name;
            CategoryId = dto.CategoryId;
            //Items = dto.Items.Select(i => new Item(i)).ToList();
        }



        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}