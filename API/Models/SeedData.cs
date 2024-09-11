using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;
using System.Net.Http.Headers;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(MvcMovieContext context)
    {
        // Look for any movies.
        if (context.Movies.Any())
        {
            return;   // DB has been seeded
        }
        context.Movies.AddRange(
            new Movie
            {
                Title = "When Harry Met Sally",
                ReleaseDate = DateOnly.Parse("1989-2-12"),
                Genres = null,
                Rating = "PG",
                Price = 7.99M
            },
            new Movie
            {
                Title = "Ghostbusters ",
                ReleaseDate = DateOnly.Parse("1984-3-13"),
                Genres = null,
                Rating = "PG",
                Price = 8.99M
            },
            new Movie
            {
                Title = "Ghostbusters 2",
                ReleaseDate = DateOnly.Parse("1986-2-23"),
                Genres = null,
                Rating = "PG",
                Price = 9.99M
            },
            new Movie
            {
                Title = "Rio Bravo",
                ReleaseDate = DateOnly.Parse("1959-4-15"),
                Genres = null,
                Rating = "R",
                Price = 3.99M
            }
        );
        context.SaveChanges();
    }
}