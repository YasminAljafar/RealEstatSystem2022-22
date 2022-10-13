using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.Models
{
    public class EarthType
    {
        public int id { get; set; }
       [ Required]
        public string Name { get; set; }
        public List<Land> Lands { get; set; }

        //public static implicit operator EarthType(List<EarthType> v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}