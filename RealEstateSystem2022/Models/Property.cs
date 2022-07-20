namespace RealEstateSystem2022.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Position { get; set; }
        public bool IsPuplish { get; set; }
        public decimal Space { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public decimal MeterPrice { get; set; }
    }
}
