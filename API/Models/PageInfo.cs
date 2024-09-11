namespace MvcMovie.Models;

public class PageInfo
{
    public required int PageNumber { get; set; }

    public required List<Movie> Movies { get; set; }

    public required int PageCount { get; set; }

    public required int MovieCount { get; set; }
}