using BitirmeMovieStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BitirmeMovieStore.Entities
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
    }
}
