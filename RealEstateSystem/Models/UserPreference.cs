using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateSystem.Models
{
    public class UserPreference
    {
        public int Id { get; set; }

        [ForeignKey("PropertyId")]
        public int PropertyId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }
    }
}
