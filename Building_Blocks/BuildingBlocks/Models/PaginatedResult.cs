namespace BuildingBlocks.Models
{
    public class PaginatedResult<T>
    {
        public IQueryable<T> Items { get; set; } = new List<T>().AsQueryable();
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
