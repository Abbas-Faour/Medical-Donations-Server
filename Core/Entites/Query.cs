namespace Core.Entites
{
    public class Query
    {
        public string Search { get; set; }
        public string Category { get; set; }
        public string SortBy { get; set; }
        public string UserEmail { get; set; }
        public bool IsSortAscending { get; set; }

        public byte PageSize { get; set; }
        public int Page { get; set; }
    }
}