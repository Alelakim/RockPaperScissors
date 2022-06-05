namespace RockPaperScissors.Models
{
    public class GameResponse
    {
        public Guid Id { get; set; }
        public List<string>? Players { get; set; }
        public Result? Result { get; set; }
        public string? ErrorInfo { get; set; }

    }
}