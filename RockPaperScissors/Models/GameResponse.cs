namespace RockPaperScissors.Models
{
    public class GameResponse
    {
        public Guid Id { get; set; }
        public List<string>? Players { get; set; }
        public string? Result { get; set; }
        public string errorInfo { get; set; }

    }
}