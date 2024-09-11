namespace MvcMovie.Models;

public class GenrePageInfo
{
    public required int PageNumber { get; set; }

    public required List<Genre> Genres { get; set; }

    public required int PageCount { get; set; }

    public required int GenreCount { get; set; }
}
