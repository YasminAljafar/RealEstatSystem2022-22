using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateSystem2022.Models
{
    public class Advertising
    {
        public int Id { get; set; }
        [Required]
        public byte[] Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public DateTime PublishDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public int Count { get; set; }

        public bool Status { get; set; }

        public ApplicationUser User { get; set; }

    }
}
