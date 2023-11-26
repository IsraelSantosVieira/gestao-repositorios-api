namespace RepositorioApp.Utilities.Paging
{
    public interface IPagination
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
        string SortField { get; set; }
        string SortType { get; set; }
        int MaxPageSize { get; set; }
    }
}
