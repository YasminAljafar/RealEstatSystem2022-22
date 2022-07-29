using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateSystem.Models
{
    public class PropertyUser
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
       
        public int PropertyId { get; set; }

        public Property Property { get; set; }

        public DateTime PuplishDate { get; set; }

        public DateTime ExpiryDate { get; set; }

    }
}
