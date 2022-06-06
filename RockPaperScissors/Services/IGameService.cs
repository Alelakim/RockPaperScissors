using RockPaperScissors.Models;

namespace RockPaperScissors.Services
{
    public interface IGameService
    {
        GameResponse GetGameAsync(Guid id);
        GameResponse CreateNewGame(string name);
        GameResponse JoinGameAsync(Guid id, string name);
        GameResponse MakeAMoveAsync(Guid id, string name, string move);
    }
}
