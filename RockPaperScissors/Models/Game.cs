namespace RockPaperScissors.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<Player>? Players { get; set; }
        public string? State { get; set; }
        public string? Result { get; set; }


    }
}
