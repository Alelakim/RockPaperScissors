using RockPaperScissors.Models;

namespace RockPaperScissors.Repository
{
    public interface IGameRepository
    {
        Guid AddGame(Game game);
        Game Find(Guid id);
        void UpdateGame(Game game);
    }
}
