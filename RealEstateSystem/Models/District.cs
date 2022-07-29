using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.Models
{ 
    //الحي
    public class District
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public List<Property> Properties { get; set; }
    }
}
