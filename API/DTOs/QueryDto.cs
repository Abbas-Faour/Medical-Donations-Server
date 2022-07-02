namespace API.DTOs
{
    public class QueryDto
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