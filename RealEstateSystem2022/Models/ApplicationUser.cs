using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;

namespace RealEstateSystem2022.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        //[Required, Maxlength(50)]
        //public string UserName { get; set; }


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

        [ Maxlength(300)]
        public string Description { get; set; }

        public bool Activ { get; set; }

        public DateTime RegisterDate { get; set; }

        public UserType UserType { get; set; }
        public List<Advertising> Advertisings { get; set; }


    }
}
