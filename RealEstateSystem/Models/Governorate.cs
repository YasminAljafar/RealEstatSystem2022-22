using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.Models
{
    public class Governorate
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<City> Cities { get; set; }

    }
}
