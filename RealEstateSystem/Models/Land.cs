using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.Models
{
    // (بياناالعقارات الزراعية(أراضي 
    public class Land
    {
        public int Id { get; set; }

        //هل الارض مشجرة؟
        [Required(ErrorMessage = "{0} is Required")]
        public bool IsTimberLand { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public decimal Length { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public decimal Width { get; set; }
        //هل يوجد بها بئر ماء؟
        [Required(ErrorMessage = "{0} is Required")]
        public bool WaterWell { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public bool WaterWellAtNear { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        //هل الأرض صالحة للبناء
        public bool LandForConstruction { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        //هل الأرض صالحة للزرعة
        public bool LandIsArable { get; set; }


        public int EarthTypeId { get; set; }
        public EarthType EarthType { get; set; }

        
        public int PropertyId { get; set; }

        public Property Property { get; set; }
    }
}
