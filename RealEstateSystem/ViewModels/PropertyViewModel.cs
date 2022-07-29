using RealEstateSystem.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.ViewModels
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        [DisplayName("Title")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public decimal Position { get; set; }
        //    public bool IsPuplish { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public decimal Space { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public int Age { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public string Description { get; set; }
        //public decimal MeterPrice { get; set; }
        [Display(Name = "District")]
        public int DistrictId { get; set; }
        public IEnumerable<District> Districts { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }
        public IEnumerable<City> Cities { get; set; }

        [Display(Name = "Governorate")]
        public int GovernorateId { get; set; }
        public IEnumerable<Governorate> Governorates { get; set; }


        [Display(Name = "Property Type")]
        public int PropertyTypeId { get; set; }
        public IEnumerable<PropertyType> PropertyTypes { get; set; }
        [Display(Name = "Property Status")]
        public int PropertyStatusId { get; set; }
        public IEnumerable<PropertyStatus> PropertyStatuses { get; set; }
        [Display(Name = "Operation Type")]
        public int OperationTypeId { get; set; }
        public IEnumerable<OperationType> OperationTypes { get; set; }

        public List<PropertyImage> propertyImages { get; set; }

        public Land Land { get; set; }
        public Home Home { get; set; }
        public Commerial Commerial { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public List<PropertyUser> PropertyUsers { get; set; }

        //هل الارض مشجرة؟
        //[Required(ErrorMessage = "{0} is Required")]
        //public bool IsTimberLand { get; set; }


    }
}
