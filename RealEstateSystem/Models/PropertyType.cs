using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.Models
{
    public class PropertyType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsPuplish { get; set; }

        public List<Property> Properties { get; set; }
    }
}