using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem.ViewModels
{
    public class AdvertisigViewModel
    {
        public int Id { get; set; }
       
        [Required(ErrorMessage = "{0} is Required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        [MaxLength(1000)]
        public string Description { get; set; }

        public DateTime PublishDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public bool Status { get; set; }
        public int Count { get; set; }

        public int ApplicationUserId { get; set; }

        public IFormFile ImageFile { get; set; }
       
    }
}
