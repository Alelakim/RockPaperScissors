using System.ComponentModel.DataAnnotations;

namespace RockPaperScissors.Models
{
    public class Result
    {
        public int Id { get; set; }
        public Player Winner { get; set; }
        public Player Loser { get; set; }
        public bool Draw { get; set; } = false;
    }
}
