using RealEstateSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.ViewModels
{
    public class PropertyUserViewModel
    {

        [Required]
        public string FirstName { get; set; }
        [Required, System.ComponentModel.DataAnnotations.StringLength(maximumLength: 50)]
        public string LastName { get; set; }
        public byte[] Image { get; set; }
        public DateTime birthday { get; set; }
        [Required]
        public bool Gender { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }




        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Space { get; set; }

        public decimal lat { get; set; }

        public decimal lng { get; set; }

        public DateTime datetime { get; set; }

      
        public int DistrictId { get; set; }
        public District District { get; set; }
        public int PropertyTypeId { get; set; }
        public PropertyType PopertType { get; set; }
        public int PropertyStatusId { get; set; }
        public PropertyStatus PropertyStatus { get; set; }
        public int OperationTypeId { get; set; }
        public OperationType OperationType { get; set; }
        public List<PropertyImage> propertyImages { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
        public List<PropertyUser> PropertyUsers { get; set; }
        public int UserTypeId { get; set; }
        public List<UserType> UserType { get; set; }
        public Land Land { get; set; }
        public Home Home { get; set; }
        public Commerial Commerial { get; set; }

        public string CreatedBy { get; set; }

    }
}
