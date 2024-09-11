using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MvcMovie.Models;
[Table("Genre")]
public class Genre
{
    public int Id { get; set; }

    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    [Required(AllowEmptyStrings = false)]
    [StringLength(30)]
    public string? Name { get; set; }

    [JsonIgnore]
    public ICollection<Movie>? Movies { get; set; }
}
