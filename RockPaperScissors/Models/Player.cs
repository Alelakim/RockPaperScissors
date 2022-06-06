namespace RockPaperScissors.Models
{
    public class Player
    {
        public Guid PlayerId { get; set; }
        public string? Name { get; set; }
        public string? Move { get; set; }

        public Guid GameId { get; set; }
        public Game? Game { get; set; }
    }
}
