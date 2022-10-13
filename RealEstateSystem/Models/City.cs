using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int GovernorateId { get; set; }
        public Governorate Governorate { get; set; }
        public List<District> Districts { get; set; }

    }
}
