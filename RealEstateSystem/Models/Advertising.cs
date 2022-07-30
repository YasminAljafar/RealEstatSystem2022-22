using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.Models
{
    public class Advertising
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public string ImageURL { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        [MaxLength(1000)]
        public string Description { get; set; }

        public DateTime PublishDate { get; set; }
        //public DateTime ExpiryDate { get; set; }

        //public int Count { get; set; }
        //[Required(ErrorMessage = "{0} is Required")]
        //public bool Status { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
