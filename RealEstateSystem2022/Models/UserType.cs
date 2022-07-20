using System.Collections.Generic;

namespace RealEstateSystem2022.Models
{
    public class UserType
    {
        public int Id { get; set; } 
        
        public string Name { get; set; }    

      
        public List<ApplicationUser> Users { get; set; }
    }
}