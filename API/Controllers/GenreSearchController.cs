using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using NuGet.Protocol;
using System.Collections.Generic;
using System.Linq;

namespace API;

[ApiController]
[Route("/api/genres/search")]
public class GenreSearchController : ControllerBase
{
    private readonly MvcMovieContext _context;

    public GenreSearchController(MvcMovieContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<GenrePageInfo> SearchGenres([FromBody] GenreSearchOptions search)
    {
        IQueryable<Genre> query = _context.Genres.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search.Name))
            query = query.Where(genre => genre.Name != null ? genre.Name.Contains(search.Name) : genre.Name == search.Name);

        var genreCount = await query.CountAsync();
        var pageCount = ((genreCount + search.PageSize) - 1) / search.PageSize;
        var pageNumber = search.Page;

        if (pageNumber > pageCount)
        {
            pageNumber = pageCount;
        }

        if (pageNumber < 1)
        {
            pageNumber = 1;
        }
        var offset = search.PageSize * (pageNumber - 1);

        query = SortGenres(query, search.SortDirection).Skip(offset).Take(search.PageSize);


        GenrePageInfo data = new GenrePageInfo
        {
            GenreCount = genreCount,
            PageCount = pageCount,
            PageNumber = pageNumber,
            Genres = await query.ToListAsync()
        };

        return data;
    }

    private IQueryable<Genre> SortGenres(IQueryable<Genre> query, GenreSortDirection sortDirection) => sortDirection switch
    {
        GenreSortDirection.Ascending => query.OrderBy(i => i.Name),
        GenreSortDirection.Descending => query.OrderByDescending(i => i.Name),
    };

    /*[HttpPost]
    public async Task<List<Movie>> SearchMovies([FromBody] MovieSearchOptions search)
    {
        IQueryable<Movie> query = _context.Movies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search.Title))
            query = query.Where(movie => movie.Title != null ? movie.Title.Contains(search.Title): movie.Title== search.Title);

        if (search.Genre != null)
            query = query.Where(movie => movie.Genre == search.Genre);

        if (search.ReleasedAfter != null)
            query = query.Where(movie => movie.ReleaseDate >= search.ReleasedAfter);

        return await query.ToListAsync();
    }*/
}

public record GenreSearchOptions
{
    public int Page { get; set; }
    public int PageSize { get; set; } = 10;
    public string? Name { get; set; }
    public GenreSortDirection SortDirection { get; set; }
}

public enum GenreSortDirection
{
    Ascending = 0,
    Descending = 1,
}