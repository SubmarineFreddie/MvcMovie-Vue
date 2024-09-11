using API.Extensions;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using System.Linq.Expressions;

namespace API;

[ApiController]
[Route("/api/movies/search")]
public class MovieSearchController : ControllerBase
{
    private readonly MvcMovieContext _context;

    public MovieSearchController(MvcMovieContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<PageInfo> SearchMovies([FromBody] MovieSearchOptions search)
    {
        IQueryable<Movie> query = _context.Movies.Include(m => m.Genres).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search.Title))
            query = query.Where(movie => movie.Title.Contains(search.Title));

        if (search.GenreIds?.Length > 0)
            query = query.Where(movie => movie.Genres!.Any(g => search.GenreIds.Contains(g.Id)));

        if (search.ReleasedAfter != null)
            query = query.Where(movie => movie.ReleaseDate >= search.ReleasedAfter);

        var movieCount = await query.CountAsync();
        var pageCount = ((movieCount + search.PageSize) - 1) / search.PageSize;
        var pageNumber = search.Page;

        if (pageNumber > pageCount)
            pageNumber = pageCount;

        if (pageNumber < 1)
            pageNumber = 1;

        var offset = search.PageSize * (pageNumber - 1);

        query = SortMovies(query, search.SortType, search.SortDirection)
            .Skip(offset)
            .Take(search.PageSize);

        return new PageInfo
        {
            MovieCount = movieCount,
            PageCount = pageCount,
            PageNumber = pageNumber,
            Movies = await query.ToListAsync()
        };
    }

    private IQueryable<Movie> SortMovies(IQueryable<Movie> query, MovieSortType sortType, SortDirection sortDirection) => sortType switch
    {
        MovieSortType.None => query.OrderBy(i => i.Id),
        MovieSortType.Title => query.OrderByWithDirection(sortDirection, m => m.Title),
        MovieSortType.ReleaseDate => query.OrderByWithDirection(sortDirection, i => i.ReleaseDate),
        MovieSortType.Genre =>  query.OrderByWithDirection(sortDirection, i => i.Genres!.Count()),
        MovieSortType.Price => query.OrderByWithDirection(sortDirection, i => i.Price),
        MovieSortType.Rating => query.OrderByWithDirection(sortDirection, i => i.Rating)
    };
}

public record MovieSearchOptions
{
    public int Page { get; set; }
    public int PageSize { get; set; } = 10;
    public string? Title { get; set; }
    public int[]? GenreIds { get; set; }
    public DateOnly? ReleasedAfter { get; set; }
    public MovieSortType SortType { get; set; }
    public SortDirection SortDirection { get; set; }
}

public enum MovieSortType
{
    None = 0,
    Title = 1,
    ReleaseDate = 2,
    Genre = 3,
    Rating = 4,
    Price = 5
}

//public static class Extensions
//{
//    public static bool ContainsAny<T>(this IQueryable<T> haystacks, IQueryable<T> needles)
//    {
//        foreach (var needle in needles)
//        {
//            if (haystacks.Contains(needle))
//                return true;
//        }

//        return false;
//    }
//}