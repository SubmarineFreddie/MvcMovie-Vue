using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;
[Table("Movie")]
public class Movie
{
    public int Id { get; set; }

    [StringLength(60, MinimumLength = 3)]
    [Required(AllowEmptyStrings = false)]
    public required string Title { get; set; }

    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public required DateOnly ReleaseDate { get; set; }

    [Range(1, 100)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public required decimal Price { get; set; }

    public ICollection<Genre>? Genres { get; set; }

    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
    [StringLength(5)]
    [Required(AllowEmptyStrings = false)]
    public string? Rating { get; set; }

    public string? Summary { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string? ImageUrl { get; set; }
}
