﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.Models
{
    public class UserType
    {
        public int Id { get; set; } 
        
        [Required]
        public string Name { get; set; }    

      
        public List<ApplicationUser> Users { get; set; }
        public List<Property> Properties { get; set; }
    }
}