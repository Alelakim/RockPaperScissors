namespace RockPaperScissors.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public Player? PlayerOne { get; set; }
        public Player? PlayerTwo { get; set; }
        public string? Result { get; set; }

    }
}