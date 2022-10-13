using Microsoft.AspNetCore.Http;
using RealEstateSystem.Areas.Identity.Pages.Account.Manage;
using RealEstateSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace RealEstateSystem.ViewModels
{
    public class HomeVM
    {
        //HomeVM()
        //{
        //    new List<ApplicationUser>();
        //}
        public List<Property> Properties { get; set; } = new List<Property>();

        public List<Advertising> Advertisings { get; set; } = new List<Advertising>();
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public ApplicationUser CurrentUser { get; set; }
        public byte[] Image { get; set; }

        public List<District> Districts { get; set; } = new List<District>();
        public List<City> Cities { get; set; } = new List<City>();
        public List<Governorate> Governorates { get; set; } = new List<Governorate>();
        public List<PropertyType> PropertyTypes { get; set; } = new List<PropertyType>();
        public List<OperationType> OperationTypes { get; set; } = new List<OperationType>();
        public PropertyFilterParams FilterParams { get; set; }

        public List<UserType> UserTypes { get; set; } = new List<UserType>();
        public List<PropertyUser> PropertyUsers { get; set; } = new List<PropertyUser>();


    }

    public enum PropertyOrderType
    {
        Normal = 0,
        Newer = 1,
        LowerProce = 2
    }
}
