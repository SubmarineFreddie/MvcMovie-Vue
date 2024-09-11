using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace API;

[ApiController]
[Route("/api/movies")]
public class MoviesController : ControllerBase
{
    private readonly MvcMovieContext _context;

    public MoviesController(MvcMovieContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public async Task<List<Movie>> GetMovies()
    {
        return await _context.Movies.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
        var record = await _context.Movies.FindAsync(id);
        if (record == null) {
            return NotFound("Unknown movie");
        }
        return record;
    }

    [HttpPost]
    public async Task<Movie> CreateMovie([FromBody] Movie data)
    {
        if (data.Genres != null)
        {
            var genreIds = data.Genres.Select(i => i.Id).ToHashSet();
            var databaseGenres = _context.Genres.Where(genre => genreIds.Contains(genre.Id));
            data.Genres = await databaseGenres.ToListAsync();
        }
        _context.Movies.Add(data);
        await _context.SaveChangesAsync();
        return data;
    }

    [HttpPut]
    public async Task<ActionResult<Movie>> UpdateMovie([FromBody] Movie data)
    {
        var record = await _context.Movies.Include(m => m.Genres).SingleOrDefaultAsync(movie => movie.Id == data.Id);
        if (record == null)
            return NotFound("Unknown movie");

        _context.Entry(record).CurrentValues.SetValues(data);
        if (data.Genres != null)
        {
            var genreIds = data.Genres.Select(i => i.Id).ToHashSet();
            var databaseGenres = _context.Genres.Where(genre => genreIds.Contains(genre.Id));
            record.Genres = await databaseGenres.ToListAsync();
        }
        
        
        //JsonSerializer.Serialize("abc", new JsonSerializerOptions() { ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles });

        await _context.SaveChangesAsync();
        return data;
    }

    [HttpDelete]
    public async Task<ActionResult<Movie>> DeleteMovie([FromBody] Movie data)
    {
        var record = await _context.Movies.FindAsync(data.Id);
        if (record == null)
            return NotFound("Unknown movie");

        _context.Remove(record);

        await _context.SaveChangesAsync();
        return data;
    }
}
