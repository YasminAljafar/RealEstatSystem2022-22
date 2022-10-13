using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Space { get; set; }
        public decimal lat { get; set; }
        public decimal lng { get; set; }
        public DateTime datetime { get; set; }
        public string Description { get; set; }
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
