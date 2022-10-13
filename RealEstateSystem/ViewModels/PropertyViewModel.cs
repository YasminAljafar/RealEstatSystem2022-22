using Microsoft.AspNetCore.Http;
using RealEstateSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.ViewModels
{
    public class PropertyViewModel
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
        public IEnumerable<District> Districts { get; set; }=new List<District>();
        public int CityId { get; set; }
        public City City { get; set; }
        public IEnumerable<City> Cities { get; set; } = new List<City>();
        public int GovernorateId { get; set; }
        public Governorate Governorate { get; set; }
        public IEnumerable<Governorate> Governorates { get; set; } = new List<Governorate>();
        public int PropertyTypeId { get; set; }
        public PropertyType PopertType { get; set; }
        public IEnumerable<PropertyType> PropertyTypes { get; set; }
        public int PropertyStatusId { get; set; }
        public PropertyStatus PropertyStatus { get; set; }
        public IEnumerable<PropertyStatus> PropertyStatuses { get; set; }
        public int OperationTypeId { get; set; }
        public OperationType OperationType { get; set; }
        public IEnumerable<OperationType> OperationTypes { get; set; }
        public Land Land { get; set; }
        public Home Home { get; set; }
        public Commerial Commerial { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
        public List<PropertyUser> PropertyUsers { get; set; }
        public List<PropertyImage> propertyImages { get; set; } = new List<PropertyImage>();
        //multiple images 
        public List<IFormFile> Images { get; set; }
        public Property Property { get; set; }
        //هل الارض مشجرة؟
        public bool IsTimberLand { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        //هل يوجد بها بئر ماء؟
        public bool WaterWell { get; set; }
        //[Required(ErrorMessage = "{0} is Required")]
        public bool WaterWellAtNear { get; set; }
        //هل الأرض صالحة للبناء
        public bool LandForConstruction { get; set; }
        //هل الأرض صالحة للزرعة
        public bool LandIsArable { get; set; }
        public int EarthTypeId { get; set; }
        public EarthType EarthType { get; set; }
        public IEnumerable<EarthType> EarthTypes { get; set; }
        public int PropertyId { get; set; }
        public IEnumerable<Property> Properties { get; set; }

        //home ملاااحظةةة الحقل الي نوعه إنت بشكل افتراضي بيكون مطلوووووب
        public int RoomCount { get; set; }
        public int Floor { get; set; }
        public bool IsSunny { get; set; }
        public int PropertyId1 { get; set; }
        public string ApplicationUserId { get; set; }
        public string ApplicationUserName { get; set; }
        public string CreatedBy { get; set; }
    }
}
