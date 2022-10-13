using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.Models
{
    public class Home
    {
        public int Id { get; set; }

     
        public int  RoomCount { get; set; }
     

        public int Floor { get; set; }

        public bool IsSunny { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
