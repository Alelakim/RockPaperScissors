using RockPaperScissors.Models;

namespace RockPaperScissors.Services
{
    public interface IRockPaperScissorService
    {
        Result RunGame(List<Player> players);
    }
}
