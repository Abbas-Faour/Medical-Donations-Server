namespace API.DTOs
{
     public class QueryResultDto<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}