using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using NuGet.Protocol;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API;

[ApiController]
[Route("/api/genres")]
public class GenresController : ControllerBase
{
    private readonly MvcMovieContext _context;

    public GenresController(MvcMovieContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public async Task<List<Genre>> GetGenres()
    {
        return await _context.Genres.OrderBy(i => i.Name).ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Genre>> GetGenre(int id)
    {
        var record = await _context.Genres.FindAsync(id);
        if (record == null)
        {
            return NotFound("Unknown genre");
        }
        return record;
    }

    [HttpPost]
    public async Task<Genre> CreateGenre([FromBody] Genre data)
    {
        _context.Genres.Add(data);
        await _context.SaveChangesAsync();
        return data;
    }

    [HttpPut]
    public async Task<ActionResult<Genre>> UpdateGenre([FromBody] Genre data)
    {
        var record = await _context.Genres.FindAsync(data.Id);
        if (record == null)
            return NotFound("Unknown genre");

        _context.Entry(record).CurrentValues.SetValues(data);

        await _context.SaveChangesAsync();
        return data;
    }

    [HttpDelete]
    public async Task<ActionResult<Genre>> DeleteGenre([FromBody] Genre data)
    {
        var record = await _context.Genres.FindAsync(data.Id);
        if (record == null)
            return NotFound("Unknown genre");

        _context.Remove(record);

        await _context.SaveChangesAsync();
        return data;
    }
}
