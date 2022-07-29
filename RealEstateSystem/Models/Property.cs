using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.Models
{
    public class Property
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public decimal Price { get; set; }
        //[Required(ErrorMessage = "{0} is Required")]
        //public decimal Position { get; set; }
        //public bool IsPuplish { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public decimal Space { get; set; }
        //[Required(ErrorMessage = "{0} is Required")]
        //public int Age { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public string Description { get; set; }
        //public decimal MeterPrice { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        public int PropertyTypeId { get; set; }
        public PropertyType PopertType { get; set; }

        public int PropertyStatusId { get; set; }
        public PropertyStatus PropertyStatus { get; set; }

        public int OperationTypeId { get; set; }
        public OperationType OperationType { get; set; }

        public List<PropertyImage> propertyImages { get; set; }

        public Land Land { get; set; }
        public Home Home { get; set; }
        public Commerial Commerial { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public List<PropertyUser> PropertyUsers { get; set; }
        //public int CityId { get; internal set; }
        //public int GovernorateId { get; internal set; }
    }
}
