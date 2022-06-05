using System.ComponentModel.DataAnnotations;

namespace RockPaperScissors.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Move { get; set; }
    }
}
