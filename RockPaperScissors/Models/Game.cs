namespace RockPaperScissors.Models
{
    public class Game
    {
        public Guid GameId { get; set; }
        public List<Player>? Players { get; set; }
        public Result? Result { get; set; }

    }
}
