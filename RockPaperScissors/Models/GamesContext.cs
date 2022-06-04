using Microsoft.EntityFrameworkCore;

namespace RockPaperScissors.Models
{
    public class GamesContext : DbContext
    {
        public GamesContext(DbContextOptions<GamesContext> options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; } = null!;
    }
}
