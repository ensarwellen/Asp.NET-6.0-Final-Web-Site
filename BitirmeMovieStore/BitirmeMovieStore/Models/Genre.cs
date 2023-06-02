using System.ComponentModel.DataAnnotations;

namespace BitirmeMovieStore.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string? GenreName { get; set; }
    }
}
