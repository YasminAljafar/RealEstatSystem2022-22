namespace RealEstateSystem.ViewModels
{
    public class PropertyFilterParams
    {
        public int? PropertyType { get; set; }
        public int? OperationType { get; set; }
        public int? City { get; set; }
        public int? District { get; set; }
        public string roomcount { get; set; }
        public decimal? price { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get { return 10; } }
        public PropertyOrderType OrderType { get; set; }

    }
}
