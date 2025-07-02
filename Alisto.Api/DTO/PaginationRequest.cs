namespace Alisto.Api.DTOs
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
        public string? SearchTerm { get; set; }
    }
}