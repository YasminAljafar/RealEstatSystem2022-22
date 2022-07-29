using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.Models
{
    public class PropertyImage
    {
        public int id { get; set; }
        [Required]
        public string imageUrl { get; set; }
        [Required]
        public string Description { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
