using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.Models
{
    // البيانات التجارية للعقارات
    public class Commerial
    {
        public int Id { get; set; } 
     
        public decimal Height { get; set; }

        [Required(ErrorMessage = "{0} is Required")]
        public decimal Width { get; set; }

        [Required(ErrorMessage = "{0} is Required")]
        public decimal Length { get; set; }
       
        //public int Floor { get; set; }
      
        //public  bool  Bathroom{ get; set; }
     
        //public bool Buffet { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
