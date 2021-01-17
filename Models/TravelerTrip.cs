namespace WindowsFront_end.Models
{
    public class TravelerTrip
    {
        public int Id { get; }
        public Person Traveler { get; set; }
        public Trip Trip { get; set; }

    }
}
