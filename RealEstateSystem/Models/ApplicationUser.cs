using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RealEstateSystem.Models
{
    public class ApplicationUser : IdentityUser
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
        public bool Activ { get; set; }
        public DateTime RegisterDate { get; set; }
        [DisplayName("User Type")]
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }
        public List<Advertising> Advertisings { get; set; }
        public ICollection<Property> Properties { get; set; }
        public List<PropertyUser> PropertyUsers { get; set; }



    }
}
