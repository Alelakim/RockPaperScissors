namespace RockPaperScissors.Models
{
    public class Result
    {
        public Player? Winner { get; set; }
        public Player? Loser { get; set; }
        public bool? Draw { get; set; } = false;
    }
}
