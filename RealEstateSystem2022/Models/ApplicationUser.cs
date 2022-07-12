using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System;

namespace RealEstateSystem2022.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required, Maxlength(50)]
        public string FirstName { get; set; }

        [Required, Maxlength(50)]
        public string LastName { get; set; }

        [ Maxlength(500)]
        public byte[] Image { get; set; }

        public DateTime birthday { get; set; }

        public bool Gender { get; set; }

        [ Maxlength(50)]
        public string Country { get; set; }

        [ Maxlength(50)]
        public string City { get; set; }

        [ Maxlength(200)]
        public string Description { get; set; }

        public bool Activ { get; set; }

        public DateTime RegisterDate { get; set; }

    }
}
