using RockPaperScissors.Models;

namespace RockPaperScissors.Services
{
    public interface IGameService
    {
        GameResponse GetGame(Guid id);
        GameResponse CreateNewGame(string name);
        GameResponse JoinGame(Guid id, string name);
        GameResponse MakeAMove(Guid id, string name, string move);
    }
}
