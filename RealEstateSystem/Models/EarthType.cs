using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.Models
{
    public class EarthType
    {
        public int id { get; set; }
       [ Required]
        public int Name { get; set; }
        public List<Land> Lands { get; set; }
    }
}