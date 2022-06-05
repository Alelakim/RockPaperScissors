namespace RockPaperScissors.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public List<Player> Players { get; set; }
        public Result Result { get; set; }


    }
}
