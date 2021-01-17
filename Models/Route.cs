using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WindowsFront_end.Models.DTO_s;

namespace WindowsFront_end.Models
{
    public class Route : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int RouteId { get; set; }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged("Description"); }
        }
        public List<Destination> Destinations { get; set; } = new List<Destination>();


        public Route(string description)
        {
            Description = description;
        }

        public Route(RouteDTO.Overview dto)
        {
            Description = dto.Description;
            RouteId = dto.RouteId;
            Destinations = dto.Destinations.Select(d => new Destination(d)).ToList();
        }

        public Route()
        {

        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}