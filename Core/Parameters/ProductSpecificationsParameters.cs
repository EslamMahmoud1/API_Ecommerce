namespace Core.Parameters
{
    public class ProductSpecificationsParameters
    {
        private const int MAXPAGESIZE = 10; 
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingValues? Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _PageSize = 5;

        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value > MAXPAGESIZE ? MAXPAGESIZE : value; }
        }
        private string? _search;

        public string? Search
        {
            get { return _search; }
            set { _search = value?.Trim().ToLower(); }
        }

    }
}
